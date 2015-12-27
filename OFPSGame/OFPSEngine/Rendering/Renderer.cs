using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OFPSEngine.ResourceManagement;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;

namespace OFPSEngine.Rendering
{
    public class Renderer
    {
        internal Device Device { get; private set; }
        internal Factory Factory { get; private set; }
        internal DeviceContext Context { get; private set; }            
        private Effect shader;
        private InputLayout layout;

        private static Renderer current;        

        public static Renderer Current
        {
            get { return current ?? (current = new Renderer()); }
        }

        private Renderer()
        {
#if DEBUG
            Device = new Device(DriverType.Hardware, DeviceCreationFlags.SingleThreaded | DeviceCreationFlags.Debug);
#else
            Device = new Device(DriverType.Hardware, DeviceCreationFlags.SingleThreaded);
#endif
            Factory = new Factory();
            Context = Device.ImmediateContext;

            var bc = ShaderBytecode.Compile(Resources.Shader, "fx_5_0", ShaderFlags.None, EffectFlags.None);
            shader = new Effect(Device, bc);

            var sig = shader.GetTechniqueByIndex(0).GetPassByIndex(0).Description.Signature;
            Context.InputAssembler.InputLayout = layout = new InputLayout(Device, sig, Vertex.GetElements());
        }

        public void DrawModel3D(Model3DResource model, DrawInfo info)
        {
            if (model.VertexBuffer == null)
            {
                var vertices = new List<Vertex>();
                for (int i = 0; i < model.ModelData.Meshes[0].VertexCount; i++)
                {
                    var position = model.ModelData.Meshes[0].Vertices[i];
                    var normal = model.ModelData.Meshes[0].Normals[i];
                    var uv = model.ModelData.Meshes[0].TextureCoordinateChannels[0][i];
                    var tangent = model.ModelData.Meshes[0].Tangents[i];
                    var binormal = model.ModelData.Meshes[0].BiTangents[i];

                    var v = new Vertex(new Vector3(-position.X, position.Z, position.Y),
                        new Vector3(-normal.X, normal.Z, normal.Y), new Vector2(uv.X, 1f - uv.Y),
                        new Vector3(-tangent.X, tangent.Z, tangent.Y), new Vector3(-binormal.X, binormal.Z, binormal.Y));

                    vertices.Add(v);
                }                              

                var indices = model.ModelData.Meshes[0].GetIndices();             

                model.VertexBuffer = Buffer.Create(Device, BindFlags.VertexBuffer, vertices.ToArray());
                model.IndexBuffer = Buffer.Create(Device, BindFlags.IndexBuffer, indices);
                model.VertexCount = vertices.Count;
                model.IndexCount = indices.Length;
            }

            Context.InputAssembler.InputLayout = layout;
            Context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            Context.InputAssembler.SetVertexBuffers(0, new[] {model.VertexBuffer}, new[] {Vertex.Stride}, new[] {0});
            Context.InputAssembler.SetIndexBuffer(model.IndexBuffer, Format.R32_UInt, 0);
            shader.GetVariableByName("world").AsMatrix().SetMatrix(info.World);
            shader.GetVariableByName("viewProj").AsMatrix().SetMatrix(info.View*info.Projection);
            shader.GetVariableByName("diffuse").AsShaderResource().SetResource(info.DiffuseMap.View);
            shader.GetVariableByName("normal").AsShaderResource().SetResource(info.NormalMap.View);            
            shader.GetVariableByName("campos").AsVector().Set(info.CameraPosition);
            shader.GetTechniqueByIndex(0).GetPassByIndex(0).Apply(Context);

            Context.DrawIndexed(model.IndexCount, 0, 0);
        }
    }
}
