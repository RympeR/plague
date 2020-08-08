using System.Collections;
using System.Collections.Generic;

public static class torch
{
    private static int amount = 0;

    public static float CalculateSpeedSlow(float current_speed)
    {
        return current_speed - amount * 3f;
    }

    public static int IncreaseTorchAmount()
    {
        amount++;
        return amount;
    }

    public static int DecreaseTorchAmount()
    {
        amount--;
        return amount;
    }
}
