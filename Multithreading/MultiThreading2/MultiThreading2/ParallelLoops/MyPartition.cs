using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace ParallelLoops
{
    public class MyPartition<T> : Partitioner<T>
    {
        public override IList<IEnumerator<T>> GetPartitions(int partitionCount)
        {
            throw new NotImplementedException();
        }
    }
}
