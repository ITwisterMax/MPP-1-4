using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGeneratorLibrary.Model;
using TestGeneratorLibrary.Model.Class;

namespace TestGeneratorLibrary.Block
{
    /// <summary>
    ///     Get all needed information about code
    /// </summary>
    public class Analyzer
    {
        /// <summary>
        ///     Get all file information
        /// </summary>
        /// 
        /// <param name="code">Source code</param>
        /// 
        /// <returns>FileData</returns>
        public static FileData GetFileData(string code)
        {
            // Get file root
            CompilationUnitSyntax codeRoot = CSharpSyntaxTree.ParseText(code).GetCompilationUnitRoot();

            // Get all classes
            var classesData = new List<ClassData>();
            foreach (ClassDeclarationSyntax classData in codeRoot.DescendantNodes().OfType<ClassDeclarationSyntax>())
            {
                classesData.Add(GetClassData(classData));
            }

            // Format classes data
            return new FileData(classesData);
        }

        /// <summary>
        ///     Get all class information
        /// </summary>
        /// 
        /// <param name="classNode">Class node information</param>
        /// 
        /// <returns>ClassData</returns>
        private static ClassData GetClassData(ClassDeclarationSyntax classNode)
        {
            // Get constructors information
            var allConstructors = classNode.DescendantNodes().OfType<ConstructorDeclarationSyntax>()
                .Where((constructorData) => constructorData.Modifiers.Any(
                    (modifier) => modifier.IsKind(SyntaxKind.PublicKeyword)
                ));

            var constructorsData = new List<ConstructorData>();
            foreach (var constructorData in allConstructors)
            {
                constructorsData.Add(GetConstructorData(constructorData));
            }

            // Get methods information
            var allMethods = classNode.DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Where((methodData) => methodData.Modifiers.Any(
                    (modifier) => modifier.IsKind(SyntaxKind.PublicKeyword)
                ));

            var methodsData = new List<MethodData>();
            foreach (var methodData in allMethods)
            {
                methodsData.Add(GetMethodData(methodData));
            }

            // Format constructors and methods data
            return new ClassData(classNode.Identifier.ValueText, constructorsData, methodsData);
        }

        /// <summary>
        ///     Get all constructors information
        /// </summary>
        /// 
        /// <param name="constructorNode">Constructor node information</param>
        /// 
        /// <returns>ConstructorData</returns>
        private static ConstructorData GetConstructorData(ConstructorDeclarationSyntax constructorNode)
        {
            // Get all parameters
            var parameters = new Dictionary<string, string>();
            foreach (var parameter in constructorNode.ParameterList.Parameters)
            {
                parameters.Add(parameter.Identifier.Text, parameter.Type.ToString());
            }

            // Format constructor information
            return new ConstructorData(constructorNode.Identifier.ValueText, parameters);
        }

        /// <summary>
        ///     Get all methods information
        /// </summary>
        /// 
        /// <param name="methodNode">Method node information</param>
        /// 
        /// <returns>MethodData</returns>
        private static MethodData GetMethodData(MethodDeclarationSyntax methodNode)
        {
            // Get all parameters
            var parameters = new Dictionary<string, string>();
            foreach (var parameter in methodNode.ParameterList.Parameters)
            {
                parameters.Add(parameter.Identifier.Text, parameter.Type.ToString());
            }

            // Format method information
            return new MethodData(methodNode.Identifier.ValueText, parameters, methodNode.ReturnType.ToString());
        }   
    }
}
