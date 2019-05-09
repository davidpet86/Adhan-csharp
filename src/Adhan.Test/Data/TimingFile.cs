using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Adhan.Test.Data
{
    public class TimingFile
    {
        [JsonProperty("params")]
        public TimingParameters Parameters;
        public TimingInfo[] Times;
        public long Variance;

        public static TimingFile Load(string inputFile)
        {
            if (File.Exists(inputFile) == false)
            {
                return null;
            }

            TimingFile timingFile = null;

            using (StreamReader file = File.OpenText(inputFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                timingFile = (TimingFile)serializer.Deserialize(file, typeof(TimingFile));
                
            }

            return timingFile;        
        }
    }
}
