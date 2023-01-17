[LZ4]: https://github.com/MiloszKrajewski/K4os.Compression.LZ4
[Snappy]: https://github.com/aloneguid/IronSnappy
[LZF]: https://github.com/Chaser324/LZF
[Brotli]: https://github.com/XieJJ99/brotli.net


# Benchmarking

If you wish to preform this benchmarks yourself, all of the code can be found in this repository. We recommend recursively cloning the repository so that all of the nessicary submodules will be brought along. You will need to apply the patch to FishyVoice by moving fishyVoice.patch into the FishyVoice directory and then running `git apply .\fishyVoice.patch` from within that directory.

By default LZ4 encoding should be used, to change this add a Unity Scripting Defines as follows:

Uncompressed -> define FISHYVOICE_DISABLE_AUDIO_COMPRESSION

[LZ4] -> Nothing needs to be done

[LZF] -> define FISHYVOICE_CLZF_COMPRESSION

[Snappy] -> define FISHYVOICE_CLZF_COMPRESSION

[Brotli] -> define FISHYVOICE_BROTLI_COMPRESSION



Our results are summarized in the table below:

| Library | Extra Compression Time | Extra Decompression Time | Average Compression Ratio | Average Initialization Time |
| ------- | ---------------------- | ------------------------ | ------------------------- | --------------------------- |
| Uncompressed | 0μs | 0μs | 1 | 13480.4μs |
| LZ4 | 29.0859μs | 10.1048μs | 3.2446 | 12488.2μs |
| Snappy | 73.9032μs | 30.5655μs | 3.7622 | 5402.0μs | 
| LZF (hlog10) | 24.75μs | 14.33μs | 3.7877 | 3355.0μs |
| LZF (hlog11) | 21.42μs | 12.18μs | 3.8535 | 3463.8μs |
| LZF (hlog13) | 24.89μs | 12.72μs | 3.8536 | 2794.0μs |
| LZF (hlog16) | 54.10μs | 14.01μs | 3.8571 | 2954.6μs |
| Bontli | 469.51μs | 102.81μs | 7.7192 | 23269.0μ |

## Methodology

The performance benchmarks were conducted utilizing the methodology outlined in [Fish-Networking's documentation] except: the tick rate for every framework was set to 60 ticks per second, the server was run from within Unity's editor, and thirty separate client executables were launched, all on a single machine (The machine used to run the benchmarks is custom built with an Intel i7-12700k, EVGA GeForce RTX 3090 with 24GB of dedicated RAM, 32GB 2133MHz Corsair RAM, and a Samsung 980 Pro NVME SSD, running Unity 2021.3.15f1 set to build executables with the IL2CPP backend). Thirty clients were chosen since more would result in GPU throttling. Once enough samples for your liking have been collected, stop the program! The benchmark will output a CSV file with the results.
