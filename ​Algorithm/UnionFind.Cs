public class UniFind<T>//无权并查集
{
    private List<(int pre, T value)> storage;
    private int[] size; // 按大小优化 Union

    public UniFind()
    {
        storage = new List<(int, T)>();
    }

    public void MakeSet(T[] array)
    {
        if (array == null || array.Length == 0) return;
        
        int start = storage.Count;
        size = new int[start + array.Length];
        
        for (int i = 0; i < array.Length; i++)
        {
            storage.Add((start + i, array[i]));
            size[start + i] = 1;
        }
    }

    public int Find(int a)
    {
        if (a < 0 || a >= storage.Count)
            throw new ArgumentOutOfRangeException(nameof(a));
        
        if (storage[a].pre != a)
        {
            storage[a] = (Find(storage[a].pre), storage[a].value); // 路径压缩
        }
        return storage[a].pre;
    }

    public void Union(int a, int b)
    {
        int rootA = Find(a);
        int rootB = Find(b);
        
        if (rootA == rootB) return;
        
        // 按大小合并（小树合并到大树）
        if (size[rootA] >= size[rootB])
        {
            storage[rootB] = (rootA, storage[rootB].value);
            size[rootA] += size[rootB];
        }
        else
        {
            storage[rootA] = (rootB, storage[rootA].value);
            size[rootB] += size[rootA];
        }
    }

    public bool IsConnect(int a, int b)
    {
        return Find(a) == Find(b);
    }
}
