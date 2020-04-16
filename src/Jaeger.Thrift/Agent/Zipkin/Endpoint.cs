/**
 * Autogenerated by Thrift Compiler (0.13.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */

using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thrift.Collections;

using Thrift.Protocol;
using Thrift.Protocol.Entities;
using Thrift.Protocol.Utilities;


namespace Jaeger.Thrift.Agent.Zipkin
{

  /// <summary>
  /// Indicates the network context of a service recording an annotation with two
  /// exceptions.
  /// 
  /// When a BinaryAnnotation, and key is CLIENT_ADDR or SERVER_ADDR,
  /// the endpoint indicates the source or destination of an RPC. This exception
  /// allows zipkin to display network context of uninstrumented services, or
  /// clients such as web browsers.
  /// </summary>
  public partial class Endpoint : TBase
  {
    private int _ipv4;
    private short _port;
    private string _service_name;
    private byte[] _ipv6;

    /// <summary>
    /// IPv4 host address packed into 4 bytes.
    /// 
    /// Ex for the ip 1.2.3.4, it would be (1 << 24) | (2 << 16) | (3 << 8) | 4
    /// </summary>
    public int Ipv4
    {
      get
      {
        return _ipv4;
      }
      set
      {
        __isset.ipv4 = true;
        this._ipv4 = value;
      }
    }

    /// <summary>
    /// IPv4 port
    /// 
    /// Note: this is to be treated as an unsigned integer, so watch for negatives.
    /// 
    /// Conventionally, when the port isn't known, port = 0.
    /// </summary>
    public short Port
    {
      get
      {
        return _port;
      }
      set
      {
        __isset.port = true;
        this._port = value;
      }
    }

    /// <summary>
    /// Service name in lowercase, such as "memcache" or "zipkin-web"
    /// 
    /// Conventionally, when the service name isn't known, service_name = "unknown".
    /// </summary>
    public string Service_name
    {
      get
      {
        return _service_name;
      }
      set
      {
        __isset.service_name = true;
        this._service_name = value;
      }
    }

    /// <summary>
    /// IPv6 host address packed into 16 bytes. Ex Inet6Address.getBytes()
    /// </summary>
    public byte[] Ipv6
    {
      get
      {
        return _ipv6;
      }
      set
      {
        __isset.ipv6 = true;
        this._ipv6 = value;
      }
    }


    public Isset __isset;
    public struct Isset
    {
      public bool ipv4;
      public bool port;
      public bool service_name;
      public bool ipv6;
    }

    public Endpoint()
    {
    }

    public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        await iprot.ReadStructBeginAsync(cancellationToken);
        while (true)
        {
          field = await iprot.ReadFieldBeginAsync(cancellationToken);
          if (field.Type == TType.Stop)
          {
            break;
          }

          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.I32)
              {
                Ipv4 = await iprot.ReadI32Async(cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 2:
              if (field.Type == TType.I16)
              {
                Port = await iprot.ReadI16Async(cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 3:
              if (field.Type == TType.String)
              {
                Service_name = await iprot.ReadStringAsync(cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 4:
              if (field.Type == TType.String)
              {
                Ipv6 = await iprot.ReadBinaryAsync(cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            default: 
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              break;
          }

          await iprot.ReadFieldEndAsync(cancellationToken);
        }

        await iprot.ReadStructEndAsync(cancellationToken);
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
    {
      oprot.IncrementRecursionDepth();
      try
      {
        var struc = new TStruct("Endpoint");
        await oprot.WriteStructBeginAsync(struc, cancellationToken);
        var field = new TField();
        if (__isset.ipv4)
        {
          field.Name = "ipv4";
          field.Type = TType.I32;
          field.ID = 1;
          await oprot.WriteFieldBeginAsync(field, cancellationToken);
          await oprot.WriteI32Async(Ipv4, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if (__isset.port)
        {
          field.Name = "port";
          field.Type = TType.I16;
          field.ID = 2;
          await oprot.WriteFieldBeginAsync(field, cancellationToken);
          await oprot.WriteI16Async(Port, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if (Service_name != null && __isset.service_name)
        {
          field.Name = "service_name";
          field.Type = TType.String;
          field.ID = 3;
          await oprot.WriteFieldBeginAsync(field, cancellationToken);
          await oprot.WriteStringAsync(Service_name, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if (Ipv6 != null && __isset.ipv6)
        {
          field.Name = "ipv6";
          field.Type = TType.String;
          field.ID = 4;
          await oprot.WriteFieldBeginAsync(field, cancellationToken);
          await oprot.WriteBinaryAsync(Ipv6, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        await oprot.WriteFieldStopAsync(cancellationToken);
        await oprot.WriteStructEndAsync(cancellationToken);
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override bool Equals(object that)
    {
      var other = that as Endpoint;
      if (other == null) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.ipv4 == other.__isset.ipv4) && ((!__isset.ipv4) || (System.Object.Equals(Ipv4, other.Ipv4))))
        && ((__isset.port == other.__isset.port) && ((!__isset.port) || (System.Object.Equals(Port, other.Port))))
        && ((__isset.service_name == other.__isset.service_name) && ((!__isset.service_name) || (System.Object.Equals(Service_name, other.Service_name))))
        && ((__isset.ipv6 == other.__isset.ipv6) && ((!__isset.ipv6) || (TCollections.Equals(Ipv6, other.Ipv6))));
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        if(__isset.ipv4)
          hashcode = (hashcode * 397) + Ipv4.GetHashCode();
        if(__isset.port)
          hashcode = (hashcode * 397) + Port.GetHashCode();
        if(__isset.service_name)
          hashcode = (hashcode * 397) + Service_name.GetHashCode();
        if(__isset.ipv6)
          hashcode = (hashcode * 397) + Ipv6.GetHashCode();
      }
      return hashcode;
    }

    public override string ToString()
    {
      var sb = new StringBuilder("Endpoint(");
      bool __first = true;
      if (__isset.ipv4)
      {
        if(!__first) { sb.Append(", "); }
        __first = false;
        sb.Append("Ipv4: ");
        sb.Append(Ipv4);
      }
      if (__isset.port)
      {
        if(!__first) { sb.Append(", "); }
        __first = false;
        sb.Append("Port: ");
        sb.Append(Port);
      }
      if (Service_name != null && __isset.service_name)
      {
        if(!__first) { sb.Append(", "); }
        __first = false;
        sb.Append("Service_name: ");
        sb.Append(Service_name);
      }
      if (Ipv6 != null && __isset.ipv6)
      {
        if(!__first) { sb.Append(", "); }
        __first = false;
        sb.Append("Ipv6: ");
        sb.Append(Ipv6);
      }
      sb.Append(")");
      return sb.ToString();
    }
  }

}