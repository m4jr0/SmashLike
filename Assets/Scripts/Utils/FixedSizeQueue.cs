using System.Collections.Generic;

public class FixedSizedQueue<T>
{
    public int size
    {
        get; private set;
    }

    private readonly object m_privateLockObject = new object();
    private readonly Queue<T> m_queue = new Queue<T>();
    private T m_lastAdded;

    public FixedSizedQueue(int size)
    {
        this.size = size;
    }

    public void Enqueue(T obj)
    {
        lock (m_privateLockObject)
        {
            m_lastAdded = obj;
            m_queue.Enqueue(obj);
        }

        lock (m_privateLockObject)
        {
            while (m_queue.Count > size)
            {
                m_queue.Dequeue();
            }
        }
    }

    public T Peek()
    {
        lock (m_privateLockObject)
        {
            return m_lastAdded;
        }
    }
}
