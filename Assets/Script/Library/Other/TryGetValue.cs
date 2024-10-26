public delegate bool TryGetValue<TResult>(out TResult result);

public delegate bool TryGetValue<in T0, TResult>(T0 value, out TResult result);

public delegate bool TryGetValue<in T0, in T1, TResult>(T0 value0, T1 value1, out TResult result);

public delegate bool TryGetValue<in T0, in T1, in T2, TResult>(T0 value0, T1 value1, T2 value2, out TResult result);

public delegate bool TryGetValue<in T0, in T1, in T2, in T3, TResult>(T0 value0, T1 value1, T2 value2, T3 value3, out TResult result);

public delegate bool TryGetValue<in T0, in T1, in T2, in T3, in T4, TResult>(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4, out TResult result);

public delegate bool TryGetValue<in T0, in T1, in T2, in T3, in T4, in T5, TResult>(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, out TResult result);

public delegate bool TryGetValue<in T0, in T1, in T2, in T3, in T4, in T5, in T6, TResult>(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, out TResult result);

public delegate bool TryGetValue<in T0, in T1, in T2, in T3, in T4, in T5, in T6, in T7, TResult>(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, out TResult result);

public delegate bool TryGetValue<in T0, in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, TResult>(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8, out TResult result);

public delegate bool TryGetValue<in T0, in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, TResult>(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8, T9 value9, out TResult result);
