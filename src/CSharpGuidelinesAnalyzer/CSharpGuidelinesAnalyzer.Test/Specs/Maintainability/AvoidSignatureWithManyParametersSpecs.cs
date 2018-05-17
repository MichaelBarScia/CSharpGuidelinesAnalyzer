﻿using CSharpGuidelinesAnalyzer.Rules.Maintainability;
using CSharpGuidelinesAnalyzer.Test.TestDataBuilders;
using Microsoft.CodeAnalysis.Diagnostics;
using Xunit;

namespace CSharpGuidelinesAnalyzer.Test.Specs.Maintainability
{
    public sealed class AvoidSignatureWithManyParametersSpecs : CSharpGuidelinesAnalysisTestFixture
    {
        protected override string DiagnosticId => AvoidSignatureWithManyParametersAnalyzer.DiagnosticId;

        [Fact]
        internal void When_method_contains_three_parameters_it_must_be_skipped()
        {
            // Arrange
            ParsedSourceCode source = new MemberSourceCodeBuilder()
                .InDefaultClass(@"
                    void M(int first, string second, double third)
                    {
                    }
                ")
                .Build();

            // Act and assert
            VerifyGuidelineDiagnostic(source);
        }

        [Fact]
        internal void When_method_contains_four_parameters_it_must_be_reported()
        {
            // Arrange
            ParsedSourceCode source = new MemberSourceCodeBuilder()
                .InDefaultClass(@"
                    void [|M|](int first, string second, double third, object fourth)
                    {
                    }
                ")
                .Build();

            // Act and assert
            VerifyGuidelineDiagnostic(source,
                "Method 'M' contains more than 3 parameters.");
        }

        [Fact]
        internal void When_deconstruct_method_contains_four_parameters_it_must_be_skipped()
        {
            // Arrange
            ParsedSourceCode source = new MemberSourceCodeBuilder()
                .InDefaultClass(@"
                    public struct S
                    {
                        public string A;
                        public string B;
                        public string C;
                        public string D;

                        public void Deconstruct(out string a, out string b, out string c, out string d)
                        {
                            throw new NotImplementedException();
                        }
                    }
                ")
                .Build();

            // Act and assert
            VerifyGuidelineDiagnostic(source);
        }

        [Fact]
        internal void When_instance_constructor_contains_three_parameters_it_must_be_skipped()
        {
            // Arrange
            ParsedSourceCode source = new TypeSourceCodeBuilder()
                .InGlobalScope(@"
                    class C
                    {
                        public C(int first, string second, double third)
                        {
                        }
                    }
                ")
                .Build();

            // Act and assert
            VerifyGuidelineDiagnostic(source);
        }

        [Fact]
        internal void When_instance_constructor_contains_four_parameters_it_must_be_reported()
        {
            // Arrange
            ParsedSourceCode source = new TypeSourceCodeBuilder()
                .InGlobalScope(@"
                    class C
                    {
                        public [|C|](int first, string second, double third, object fourth)
                        {
                        }
                    }
                ")
                .Build();

            // Act and assert
            VerifyGuidelineDiagnostic(source,
                "Constructor for 'C' contains more than 3 parameters.");
        }

        [Fact]
        internal void When_indexer_contains_three_parameters_it_must_be_skipped()
        {
            // Arrange
            ParsedSourceCode source = new MemberSourceCodeBuilder()
                .InDefaultClass(@"
                    public string this[int first, string second, double third]
                    {
                        get { throw new NotImplementedException(); }
                    }
                ")
                .Build();

            // Act and assert
            VerifyGuidelineDiagnostic(source);
        }

        [Fact]
        internal void When_indexer_contains_four_parameters_it_must_be_reported()
        {
            // Arrange
            ParsedSourceCode source = new MemberSourceCodeBuilder()
                .InDefaultClass(@"
                    public string [|this|][int first, string second, double third, object fourth]
                    {
                        get { throw new NotImplementedException(); }
                    }
                ")
                .Build();

            // Act and assert
            VerifyGuidelineDiagnostic(source,
                "Indexer contains more than 3 parameters.");
        }

        [Fact]
        internal void When_delegate_contains_three_parameters_it_must_be_skipped()
        {
            // Arrange
            ParsedSourceCode source = new TypeSourceCodeBuilder()
                .InGlobalScope(@"
                    public delegate void D(int first, string second, double third);
                ")
                .Build();

            // Act and assert
            VerifyGuidelineDiagnostic(source);
        }

        [Fact]
        internal void When_delegate_contains_four_parameters_it_must_be_reported()
        {
            // Arrange
            ParsedSourceCode source = new TypeSourceCodeBuilder()
                .InGlobalScope(@"
                    public delegate void [|D|](int first, string second, double third, object fourth);
                ")
                .Build();

            // Act and assert
            VerifyGuidelineDiagnostic(source,
                "Delegate 'D' contains more than 3 parameters.");
        }

        [Fact]
        internal void When_local_function_contains_three_parameters_it_must_be_skipped()
        {
            // Arrange
            ParsedSourceCode source = new MemberSourceCodeBuilder()
                .InDefaultClass(@"
                    void M()
                    {
                        void L(int first, string second, double third)
                        {
                        }
                    }
                ")
                .Build();

            // Act and assert
            VerifyGuidelineDiagnostic(source);
        }

        [Fact]
        internal void When_local_function_contains_four_parameters_it_must_be_reported()
        {
            // Arrange
            ParsedSourceCode source = new MemberSourceCodeBuilder()
                .InDefaultClass(@"
                    void M()
                    {
                        void [|L|](int first, string second, double third, object fourth)
                        {
                        }
                    }
                ")
                .Build();

            // Act and assert
            VerifyGuidelineDiagnostic(source,
                "Local function 'L' contains more than 3 parameters.");
        }

        protected override DiagnosticAnalyzer CreateAnalyzer()
        {
            return new AvoidSignatureWithManyParametersAnalyzer();
        }
    }
}
