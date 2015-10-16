namespace Sample.Core
{
    public static class Constants
    {
        public static class Headers
        {
            public static class Keys
            {
                public static readonly string Readonly = string.Format("{0}.Readonly", typeof(Keys));
                public static readonly string Originator = string.Format("{0}.Originator", typeof(Keys));
                public static readonly string Compressed = string.Format("{0}.Compressed", typeof(Keys));
            }

            public static class Values
            {
                public static readonly string Readonly = true.ToString();
                public static readonly string Compressed = true.ToString();
            }
        }
    }
}