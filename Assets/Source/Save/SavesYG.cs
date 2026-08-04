using System;
using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        public string Language = string.Empty;
        public DataHolder DataHolder;
        public List<RewardReciveDataTime> RewardReciveDataTimes;
    }

    [Serializable]
    public class RewardReciveDataTime
    {
        public string Id;
        public long DataTime;

        public RewardReciveDataTime(string id, long dataTime)
        {
            Id = id;
            DataTime = dataTime;
        }
    }
}