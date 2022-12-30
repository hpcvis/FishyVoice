# IronSnappy

<img src="src/IronSnappy/icon.png" width=80 height=80 align="left"/>

This is a native .NET port of [Google Snappy](https://github.com/google/snappy) compression/decompression library. The only implementation that is stable, fast, up to date with latest Snappy improvements, and most importantly *does not depend on native Snappy binaries*. Works on Windows, Linux, MacOSX, ARM and so on.

It is originally ported from the [Golang implementation](https://github.com/golang/snappy/) because Go is much easier to understand and work with comparing to C++.

The library passes *golden tests* from the original implementation i.e. compares that compression/decompression is fully compatible with the original implementation.

Internally, it is using array pooling and spans for efficient memory allocation and low GC pressure.

## Using

Reference the following NuGet package [![Nuget](https://img.shields.io/nuget/v/IronSnappy)](https://www.nuget.org/packages/IronSnappy/). You are ready to go.

To compress a buffer:

```csharp
using IronSnappy;

byte[] input = File.ReadAllBytes("TestData/Mark.Twain-Tom.Sawyer.txt");
byte[] compressed = Snappy.Encode(input);
```

To decompress a buffer:

```csharp
using IronSnappy;

byte[] input = File.ReadAllBytes("TestData/Mark.Twain-Tom.Sawyer.rawsnappy.txt")
byte[] uncompressed = Snappy.Decode(input);
```



### Streaming Format

Streams are fully supported. To decompress use `Snappy.OpenReader(Stream)` and to compress `Snappy.OpenWriter(Stream)`. Don't forget to flush 🚽 and dispose 🧻!

## Contributing

Contributions are more than welcome, just raise an issue and fire a PR. The code might have a few ugly bits due to the fact it was ported as is from Golang, you are welcome to make it prettier and/or faster.