using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Day_02
{

    internal class Utils
    {

        public static TOut[] CreateInitialisedArrayOf<TOut>(params dynamic[][] paramArrays)
        {
            // Work out the number of paramter arrays we have been given and ensure its valid.
            int numParamTypes = paramArrays.Length;
            if (numParamTypes == 0)
            {
                throw new ArgumentException("Blagh!");
            }

            // Work out how many instances we need to create 
            int numInstances = paramArrays.First().Length;
            if (numInstances == 0)
            {
                throw new ArgumentException("Blagh!");
            }

            //
            Type[] inputTypes = new Type[numParamTypes];
            for (int i = 0; i < numParamTypes; i++)
            {
                //
                if (paramArrays[i].Length != numInstances)
                {
                    throw new ArgumentException("Blagh!");
                }

                //
                inputTypes[i] = paramArrays[i][0].GetType();
                for (int j = 0; j < numInstances; j++)
                {
                    if (paramArrays[i][j].GetType() != inputTypes[i])
                    {
                        throw new ArgumentException("Blagh!");
                    }
                }
            }

            //
            TOut[] outputArray = new TOut[numInstances];
            Type outputType = typeof(TOut);
            ConstructorInfo constructor = outputType.GetConstructor(inputTypes);
            ParameterInfo[] parameters = constructor.GetParameters();


            //
            for (int i = 0; i < numInstances; i++)
            {
                object[] constructorArgs = new object[numParamTypes];

                for (int j = 0; j < numParamTypes; j++)
                {
                    constructorArgs[j] = paramArrays[i][j];
                }

                outputArray[i] = (TOut)constructor.Invoke(constructorArgs);
            }

            //
            return outputArray;
        }

    }
}
