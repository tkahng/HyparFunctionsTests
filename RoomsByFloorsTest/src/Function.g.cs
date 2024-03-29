// This code was generated by Hypar.
// Edits to this code will be overwritten the next time you run 'hypar init'.
// DO NOT EDIT THIS FILE.

using Amazon;
using Amazon.Lambda.Core;
using Hypar.Functions.Execution;
using Hypar.Functions.Execution.AWS;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace RoomsByFloorsTest
{
    public class Function
    {
        // Cache the model store for use by subsequent
        // executions of this lambda.
        private IModelStore<RoomsByFloorsTestInputs> store;

        public async Task<RoomsByFloorsTestOutputs> Handler(RoomsByFloorsTestInputs args, ILambdaContext context)
        {
            if(this.store == null)
            {
                // Preload the dependencies (if they exist),
                // so that they are available during model deserialization.
                var asmLocation = this.GetType().Assembly.Location;
                var asmDir = Path.GetDirectoryName(asmLocation);
                var asmName = Path.GetFileNameWithoutExtension(asmLocation);
                var depPath = Path.Combine(asmDir, $"{asmName}.Dependencies.dll");
                
                if(File.Exists(depPath))
                {
                    Console.WriteLine($"Loading dependencies from assembly: {depPath}...");
                    Assembly.LoadFrom(depPath);
                    Console.WriteLine("Dependencies assembly loaded.");
                }

                this.store = new S3ModelStore<RoomsByFloorsTestInputs>(RegionEndpoint.USWest1);
            }
            
            var l = new InvocationWrapper<RoomsByFloorsTestInputs,RoomsByFloorsTestOutputs>(store, RoomsByFloorsTest.Execute);
            var output = await l.InvokeAsync(args);
            return output;
        }
      }
}