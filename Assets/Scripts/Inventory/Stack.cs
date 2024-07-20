using System;

public class Stack
{
    private readonly Item _item;
    private int _count;

    public Stack(Item item, int count)
    {
        _item = item;
        _count = count;
    }

    public Item Item => _item;

    public int Count { get => _count;}

    public static Stack operator +(Stack stack, Stack other) {
        if (stack?._item == null)
            throw new ArgumentException("Нельзя прибавлять к null.");
        if (other?._item == null)
            throw new ArgumentException("Нельзя прибавлять null.");
        if (stack._item != other._item)
            throw new ArgumentException("Нельзя складывать стаки разных предметов.");
        var sum = stack._count+other._count;
        stack._count = Math.Min(sum, stack._item.MaxStackSize);
        other._count = (sum > stack._item.MaxStackSize)? sum - stack._item.MaxStackSize : 0;
        return stack;
    }

    public static Stack operator -(Stack stack, int count)
    {
        if (stack?._item == null)
            throw new ArgumentException("Нельзя прибавлять к null.");
        stack._count = Math.Max(stack._count - count, 0);
        return stack;
    }
}
