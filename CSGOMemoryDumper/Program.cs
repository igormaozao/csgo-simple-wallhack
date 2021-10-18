using CSGOMemoryDumper.MemoryHelpers;
using CSGOMemoryDumper.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace CSGOMemoryDumper {
    class Program {

        const string CSGO_PROCESS_NAME = "csgo";
        const string PATTERN_CONFIG_FILE = "patternConfig.json";
        const int BYTE_VALUE_TO_SKIP = 0x100;

        static JsonSerializerOptions jsonOptions = new JsonSerializerOptions() {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        static void Main(string[] args) {

            Console.WriteLine("Start find current game addresses...");
            Console.WriteLine("------------------------------------");

            var csProcesses = Process.GetProcessesByName(CSGO_PROCESS_NAME);
            if (csProcesses.Length == 0) {
                Console.WriteLine("CS:GO must be opened first. You do NOT need to connect to any game.");
                Console.ReadLine();
                return;
            }

            var csProcess = csProcesses[0];

            ProcessModule clientDllModule = GetClientDllModule(csProcess);
            if (clientDllModule == null) {
                Console.WriteLine("Client DLL not found inside running process.");
                Console.ReadLine();
                return;
            }

            var memReader = new MemoryReader(csProcess);
            var memScanner = new MemoryScanner(csProcess, clientDllModule);

            var patternConfig = GetPatternConfig();

            patternConfig.ForEach(config => {
                var pattern = config.GetPatternValues(BYTE_VALUE_TO_SKIP);
                var patternResult = memScanner.ScanBytes(pattern, BYTE_VALUE_TO_SKIP);

                if (patternResult.Count == 0) {
                    Console.WriteLine($"Could not find address for {config.Name}.");
                } else {
                    var address = memReader.ReadInt32(
                        patternResult.First() + config.PatternOffset) // Read the memory value in the found pattern + its offset
                        + config.ExtraBytes // Add the possible extra bytes in the read memory
                        - clientDllModule.BaseAddress.ToInt32(); //Remove the DLL base address

                    Console.WriteLine($"{config.Name}: 0x{address:X}");
                }
            });

            Console.WriteLine("------------------------------------");
            Console.WriteLine("Auto update finished!");

            Console.ReadLine();
        }

        private static ProcessModule GetClientDllModule(Process csProcess) {
            foreach (ProcessModule module in csProcess.Modules) {
                if (module.ModuleName == "client.dll")
                    return module;
            }

            return null;
        }

        private static List<PatternConfig> GetPatternConfig() {
            var fileContent = File.ReadAllText(PATTERN_CONFIG_FILE);
            return JsonSerializer.Deserialize<List<PatternConfig>>(fileContent, jsonOptions);
        }
    }
}
