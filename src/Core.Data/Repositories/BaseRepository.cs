using System;

namespace Core.Data.Repositories {
    public class BaseRepository {
        public string ConnectionString () {
            return "";
        }

        public string GetDbValue (string value) {
            return string.IsNullOrEmpty (value) ? string.Empty : value.Trim ();
        }

        public int GetDbValue (int? value) {
            return value == null || value < 0 ? 0 : Convert.ToInt32 (value);
        }
    }
}