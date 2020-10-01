using System;

namespace DataAccessTier
{
    /// <summary>
    ///     This class generates the location codes, put and pick sequences for a warehouse using the given parameters.
    ///     These location codes are then used in WMSTestDataScript to generate all locations
    ///     within the table.
    /// </summary>
    class WMSGenerateLocationsTestLayout
    {
        /// <summary>
        ///     Constructor named WMSGenerateLocationsTestLayout that contains the variables 
        ///     and for loop that generate the location code, put and pick sequences.
        /// </summary>
        public WMSGenerateLocationsTestLayout()
        {
            char c1 = 'A';

            int ii = 1;

            int PickSequence = 1;

            //int PutSequence = 3;
            //int PutSequence2 = 5;

            char c2 = 'A';

            for (int i = 1; i <= 500; i++)
            {
                if (c2 == 'F')
                {
                    c2 = 'A';
                }

                if (ii <= 9)
                {
                    Console.WriteLine(c1.ToString() + '0' + ii + c2.ToString());
                    //Console.WriteLine(PickSequence);
                }
                else
                {
                    Console.WriteLine(c1.ToString() + ii + c2.ToString());
                    //Console.WriteLine(PickSequence);
                }

                if (i % 5 == 0)
                {
                    ii++;
                    PickSequence++;
                    /*                    if (c1 == 'A')
                                        {
                    *//*                        if (!(ii % 2 == 0))
                                            {
                                                PutSequence2 += 2;
                                                Console.WriteLine(PutSequence2);
                                                Console.WriteLine(PutSequence2);
                                                Console.WriteLine(PutSequence2);
                                                Console.WriteLine(PutSequence2);
                                                Console.WriteLine(PutSequence2);
                                            }*/
                    /*                        if (ii % 2 == 0)
                                            {
                                                PutSequence += 2;
                                                Console.WriteLine(PutSequence);
                                                Console.WriteLine(PutSequence);
                                                Console.WriteLine(PutSequence);
                                                Console.WriteLine(PutSequence);
                                                Console.WriteLine(PutSequence);
                                            }*//*
                                        }*/

                }

                c2++;

                if (ii > 20)
                {
                    ii = 1;
                    c1++;
                }

            }
        }
    }
}