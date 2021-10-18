namespace CGRedSoft.Classes {
    public class Entity {
        public Client Client { get; set; }
        private int Index { get; set; }
        public float[,] WorldToScreen { get; private set; }

        public Entity(Client c, int index) {
            Client = c;
            Index = index;
            WorldToScreen = new float[4, 4];
        }

        public int GetBaseEntity() {
            return Client.Memory.ReadInt32(Client.CGClient.ToInt32() + Addresses.EntityAddress + (Addresses.EntitySizeOffSet * Index));
        }

        public bool IsDead() {
            return GetHealth() == 0;
        }

        /// <summary>
        /// Dormant flag means the entity is inactive in memory
        /// </summary>
        /// <returns></returns>
        public bool IsDormant() {
            return Client.Memory.ReadByte(GetBaseEntity() + Addresses.EntityIsDormant) == 1;
        }

        /// <summary>
        /// 2 = TR, 3 = CT
        /// </summary>
        /// <returns></returns>
        public int GetTeam() {
            return Client.Memory.ReadInt32(GetBaseEntity() + Addresses.PlayerTeamOffset);
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

            // WorldToScreen is a 4x4 matrix
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++) {
                    WorldToScreen[i, j] = Client.Memory.ReadFloat(pointer);
                    pointer += 4; //Float size
                }

            return WorldToScreen;
        }
    }
}
