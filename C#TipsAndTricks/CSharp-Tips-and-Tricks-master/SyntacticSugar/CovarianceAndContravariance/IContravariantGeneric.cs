﻿namespace CovarianceAndContravariance
{
    public interface IContravariantGeneric<in T>
    {
        // Invalid variance:
        // The type parameter 'T' must be covariantly valid on 'IContravariantGeneric<T>.Method(T)'.
        // 'T' is contravariant.
        //// T Method(T parameter);

        void Method(T parameter);
    }

    public class ContravariantGeneric<T> : IContravariantGeneric<T>
    {
        public void Method(T parameter)
        {
            System.Console.WriteLine(parameter.ToString());
        }
    }
}
