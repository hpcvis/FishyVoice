using System;
using System.Collections;
using Adrenak.UniVoice;
using UnityEngine;

public class FileAudioInput : IAudioInput {
	private class BlankComponent : MonoBehaviour { }

	public event Action<int, float[]> OnSegmentReady;

	public int Frequency => clip.frequency;

	public int ChannelCount => clip.channels;

	public int SegmentRate => 5;

	private readonly AudioClip clip;

	public FileAudioInput(AudioClip clip) {
		this.clip = clip;
		var host = new GameObject();
		host.name = "FileInputCoroutineRunner";
		host.AddComponent<BlankComponent>().StartCoroutine(SampleLoop());
	}

	private IEnumerator SampleLoop() {
		var resetTime = 1f / SegmentRate;
		var timer = resetTime;

		int realSegment = -1, virtualSegement = -1;
		var data = new float[Frequency * clip.channels / SegmentRate];
		// Debug.Log(data.Length);
		// Debug.Log(clip.samples);

		while (true) {
			timer -= Time.unscaledDeltaTime;

			if (timer <= 0) {
				// Debug.Log(Time.unscaledTime);
				timer = resetTime;

				realSegment++;
				virtualSegement++;

				var offset = realSegment * data.Length % clip.samples;
				// Debug.Log(offset);
				if (!clip.GetData(data, offset)) {
					realSegment = 0;
					timer = 0;
				} else 
					OnSegmentReady?.Invoke(virtualSegement, data);
			}

			yield return null;
		}
	}

	public void Dispose() { }
}