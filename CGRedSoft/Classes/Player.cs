namespace CGRedSoft.Classes {
    public class Player {
        public Client Client { get; set; }
        public float[,] WorldToScreen { get; private set; }

        public Player(Client c) { this.Client = c; WorldToScreen = new float[4, 4]; }

        public int GetBaseEntity() {
            return Client.Memory.ReadInt32(Client.CGClient.ToInt32() + Addresses.PlayerAddress);
        }

        public bool IsDead() {
            return GetHealth() == 0;
        }

        /// <summary>
        /// 2 = TR, 3 = CT
        /// </summary>
        /// <returns></returns>
        public int GetTeam() {
            return Client.Memory.ReadInt32(GetBaseEntity() + Addresses.PlayerTeamOffset); //2 TR, 3 CT
        }

        public int GetHealth() {
            return Client.Memory.ReadInt32(GetBaseEntity() + Addresses.PlayerHpOffset);
        }

        public Location GetLocation() {
            int pBase = GetBaseEntity();
            return new Location(Client.Memory.ReadFloat(pBase + Addresses.VecOriginOffset),
                                Client.Memory.ReadFloat(pBase + Addresses.VecOriginOffset + 4),
                                Client.Memory.ReadFloat(pBase + Addresses.VecOriginOffset + 8));
        }

        public float[,] GetWorldToScreen() {
            uint pointer = (uint)(Client.CGClient.ToInt32() + Addresses.DwVm);

            WorldToScreen[0, 0] = pointer;

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++) {
                    WorldToScreen[i, j] = Client.Memory.ReadFloat(pointer);
                    pointer += 4; //Float size
                }

            return WorldToScreen;
        }
    }
}
