namespace BinarySerializer.Tests
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Reflection.Emit;

    public abstract class MethodBuilderMethodSkelton : IWriteMethodSkeleton
    {
        private readonly string outputPath;
        private readonly string fileName;
        private AssemblyBuilder assemblyBuilder;
        private TypeBuilder typeBuilder;
                
        protected MethodBuilderMethodSkelton(string outputPath)
        {
            this.outputPath = outputPath;
            fileName = Path.GetFileName(outputPath);     
            CreateAssemblyBuilder();
            CreateTypeBuilder();
        }

        private void CreateAssemblyBuilder()
        {
            AppDomain myDomain = AppDomain.CurrentDomain;
            assemblyBuilder = myDomain.DefineDynamicAssembly(CreateAssemblyName(), AssemblyBuilderAccess.Save, Path.GetDirectoryName(outputPath));
        }

        private AssemblyName CreateAssemblyName()
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(outputPath);
            return new AssemblyName(fileNameWithoutExtension);
        }

        private void CreateTypeBuilder()
        {
            var moduleName = Path.GetFileNameWithoutExtension(outputPath);
            ModuleBuilder module = assemblyBuilder.DefineDynamicModule(moduleName, fileName);
            typeBuilder = module.DefineType("DynamicType", TypeAttributes.Public);
        }

        protected MethodBuilder CreateMethodBuilder(Type returnType, Type[] parameterTypes)
        {
            var methodBuilder = typeBuilder.DefineMethod(
                "DynamicMethod", MethodAttributes.Public | MethodAttributes.Static, returnType, parameterTypes);
            methodBuilder.InitLocals = true;
            return methodBuilder;
        }

        public abstract ILGenerator GetILGenerator<T>();
        

        public Action<BinarySerializationWriter, T> CreateDelegate<T>()
        {
            return (writer, arg2) => {};
        }

        public void Verify()
        {
            typeBuilder.CreateType();
            assemblyBuilder.Save(fileName);
            Console.WriteLine("Saving file " + fileName);
            AssemblyAssert.IsValidAssembly(outputPath);
        }
    }
    
    public class MethodBuilderWriteMethodSkeleton : MethodBuilderMethodSkelton
    {
        public MethodBuilderWriteMethodSkeleton(string outputPath)
            : base(outputPath)
        {
        }

        public override ILGenerator GetILGenerator<T>()
        {
            return CreateMethodBuilder(typeof(void), new Type[]{typeof(BinarySerializationWriter), typeof(T)}).GetILGenerator();            
        }
    }
    
    
   
}