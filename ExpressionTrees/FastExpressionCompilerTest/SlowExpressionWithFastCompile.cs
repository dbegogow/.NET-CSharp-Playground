﻿using System.Linq.Expressions;
using FastExpressionCompiler;

namespace FastExpressionCompilerTest;

public class SlowExpressionWithFastCompile
{
    public string BuildSlowExpression()
    {
        // cat => cat.SayMew(test)
        var param = Expression.Parameter(typeof(Cat), "cat");
        var variable = Expression.Constant("test");
        var body = Expression.Call(param, typeof(Cat).GetMethod(nameof(Cat.SayMew)), variable);

        var lambda = Expression.Lambda<Func<Cat, string>>(body, param);

        var func = lambda.CompileFast();

        return func(new Cat());
    }
}
