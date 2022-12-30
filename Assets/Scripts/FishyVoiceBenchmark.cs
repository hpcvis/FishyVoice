using System.IO;
using FishNet;
using FishNet.Serializing;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FishyVoice.Samples {
	

	public class FishyVoiceBenchmark : MonoBehaviour {
		public AudioClip benchmarkClip;
		private FileAudioInput input;

		private StreamWriter file;

		public void Start() {
			Random.InitState(12345); // Make sure we are getting the same set of random values every time!
			
			input = new FileAudioInput(benchmarkClip);
			input.OnSegmentReady += OnSegmentReady;
			
			if (File.Exists("report.csv"))
				File.Delete("report.csv");
			file = File.CreateText("report.csv");
		}

		public void OnApplicationQuit() => file.Close();

		void OnSegmentReady(int index, float[] samples) {
			var random = new Vector3(Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100));
			var data = new VoiceNetwork.AudioBroadcast{
				id = (short)Random.Range(0, 1000),
				segmentIndex = index,
				frequency = benchmarkClip.frequency,
				channelCount = benchmarkClip.channels,
				samples = samples,
				
				roomName = "<DEFAULT>",
				tick = (uint)Random.Range(0, 1000),
				senderID = (short)Random.Range(0, 1000),
				senderPosition = random
			};

			string displayedReport = "Encoding: ";
			var compressedWriter = new Writer();
			var uncompressedWriter = new Writer();

			{
				var encodeWatch = new System.Diagnostics.Stopwatch();
				encodeWatch.Start();
				compressedWriter.Write(data);
				encodeWatch.Stop();
				displayedReport += $"{encodeWatch.ElapsedTicks / (System.Diagnostics.Stopwatch.Frequency / (1000L*1000L))} µs, ";
				file.Write($"{encodeWatch.ElapsedTicks / (System.Diagnostics.Stopwatch.Frequency / (1000L*1000L))},");
				
				AudioBroadcastSerializer.WriteAudioBroadcastUncompressed(uncompressedWriter, data);
			}
			
			var reader = new Reader(compressedWriter.GetArraySegment(), InstanceFinder.NetworkManager);

			{
				var decodeWatch = new System.Diagnostics.Stopwatch();
				decodeWatch.Start();
				data = reader.Read<VoiceNetwork.AudioBroadcast>();
				decodeWatch.Stop();
				displayedReport += $"Decoding: {decodeWatch.ElapsedTicks / (System.Diagnostics.Stopwatch.Frequency / (1000L*1000L))} µs, ";
				file.Write($"{decodeWatch.ElapsedTicks / (System.Diagnostics.Stopwatch.Frequency / (1000L*1000L))},");
			}
			
			displayedReport += $"Compressed: {compressedWriter.Position}b, Uncompressed: {uncompressedWriter.Position}b";
			file.WriteLine($"{compressedWriter.Position},{uncompressedWriter.Position}");
			
			Debug.Log(displayedReport);
			
		}
	}
}