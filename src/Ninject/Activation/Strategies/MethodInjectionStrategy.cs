// -------------------------------------------------------------------------------------------------
// <copyright file="MethodInjectionStrategy.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2010, Enkari, Ltd.
//   Copyright (c) 2010-2017, Ninject Project Contributors
//   Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Activation.Strategies
{
    using System.Linq;
    using Ninject.Infrastructure;
    using Ninject.Planning.Directives;

    /// <summary>
    /// Injects methods on an instance during activation.
    /// </summary>
    public class MethodInjectionStrategy : ActivationStrategy
    {
        /// <summary>
        /// Injects values into the properties as described by <see cref="MethodInjectionDirective"/>s
        /// contained in the plan.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="reference">A reference to the instance being activated.</param>
        public override void Activate(IContext context, InstanceReference reference)
        {
            Ensure.ArgumentNotNull(context, "context");
            Ensure.ArgumentNotNull(reference, "reference");

            foreach (var directive in context.Plan.GetAll<MethodInjectionDirective>())
            {
                var arguments = directive.Targets.Select(target => target.ResolveWithin(context));
                directive.Injector(reference.Instance, arguments.ToArray());
            }
        }
    }
}