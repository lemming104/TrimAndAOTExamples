namespace TrimDemo.Reflection
{
    public class SelfReflectingClass1 : SelfReflector<SelfReflectingClass1>
    {
        public string? StringValue;
        public int IntValue;
        public SelfReflectingClass1? RecursiveMember;
        public SelfReflectingClass2? SelfReflectingClass2Member;
    }

    public class SelfReflectingClass2 : SelfReflector<SelfReflectingClass2>
    {
        public double DoubleValue;
    }
}
