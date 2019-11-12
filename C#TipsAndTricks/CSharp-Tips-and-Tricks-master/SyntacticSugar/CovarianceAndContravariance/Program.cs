namespace CovarianceAndContravariance
{
    using ClassHierarchy;

    // LastClass : MiddleClass : BaseClass
    public static class Program
    {
        public static void Main()
        {
            InvariantGeneric();
            ContravariantGeneric();
            CovariantGeneric();
        }

        public static void InvariantGeneric()
        {
            IInvariantGeneric<MiddleClass> genericMiddle = new InvariantGeneric<MiddleClass>();
            MiddleClass result = genericMiddle.Method(new MiddleClass());

            // This will produce compile-time error:
            // Cannot implicitly convert type 'IInvariantGeneric<MiddleClass>' to 'IInvariantGeneric<BaseClass>'.
            // An explicit conversion exists (are you missing a cast?)
            ////IInvariantGeneric<BaseClass> genericBase = genericMiddle;

            // This will produce compile-time error:
            // Cannot implicitly convert type 'IInvariantGeneric<MiddleClass>' to 'IInvariantGeneric<LastClass>'.
            // An explicit conversion exists (are you missing a cast?)
            //// IInvariantGeneric<LastClass> genericLast = genericMiddle;
        }

        public static void ContravariantGeneric()
        {
            IContravariantGeneric<MiddleClass> genericMiddle = new ContravariantGeneric<MiddleClass>();
            genericMiddle.Method(new MiddleClass());

            IContravariantGeneric<LastClass> genericMiddle1 = new ContravariantGeneric<MiddleClass>();
            genericMiddle1.Method(new LastClass());
            IContravariantGeneric<LastClass> genericMiddle2= new ContravariantGeneric<BaseClass>();
            genericMiddle2.Method(new LastClass());

            IContravariantGeneric<MiddleClass> genericMiddle3 = new ContravariantGeneric<BaseClass>();
            genericMiddle3.Method(new MiddleClass());

            IContravariantGeneric<BaseClass> genericMiddle4 = new ContravariantGeneric<BaseClass>();
            genericMiddle4.Method(new BaseClass());

            // This will produce compile-time error:
            // Cannot implicitly convert type 'IContravariantGeneric<MiddleClass>' to 'IContravariantGeneric<BaseClass>'.
            // An explicit conversion exists (are you missing a cast?)
            //// IContravariantGeneric<BaseClass> genericBase = genericMiddle;

            // This is OK here:
            IContravariantGeneric<LastClass> genericLast = genericMiddle;
            genericLast.Method(new LastClass());
        }

        public static void CovariantGeneric()
        {
            ICovariantGeneric<MiddleClass> genericMiddle = new CovariantGeneric<MiddleClass>();
            MiddleClass result = genericMiddle.Method();

            ICovariantGeneric<BaseClass> genericMiddle1 = new CovariantGeneric<MiddleClass>();
            ICovariantGeneric<BaseClass> genericMiddle2 = new CovariantGeneric<LastClass>();
            var  a = genericMiddle1.Method();
            var b = genericMiddle2.Method();


            ICovariantGeneric<MiddleClass> genericMiddle3= new CovariantGeneric<LastClass>();

            ICovariantGeneric<LastClass> genericMiddle4 = new CovariantGeneric<LastClass>();


            // This is OK here:
            ICovariantGeneric<BaseClass> genericBase = genericMiddle;
            BaseClass baseResult = genericBase.Method();

            // This will produce compile-time error:
            // Cannot implicitly convert type 'ICovariantGeneric<MiddleClass>' to 'ICovariantGeneric<LastClass>'.
            // An explicit conversion exists (are you missing a cast?)
            //// ICovariantGeneric<LastClass> genericLast = genericMiddle;
        }
    }
}