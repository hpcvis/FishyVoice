using FishNet.CodeAnalysis.Annotations;
using FishNet.Connection;
using FishNet.Object;

namespace FishyVoice {
	
	// Additions to NetworkBehaviour that make it easier to use
	public abstract class NetworkBehaviour : FishNet.Object.NetworkBehaviour {
		
		[OverrideMustCallBase(BaseCallMustBeFirstStatement = true)]
		public override void OnStartServer() {
			base.OnStartServer();
			OnStartBoth();
		}

		[OverrideMustCallBase(BaseCallMustBeFirstStatement = true)]
		public override void OnStartClient() {
			base.OnStartClient();
			OnStartBoth();
		}
		
		/// <summary>
		///		Function called when the object this component is attached to is spawned on either the client or the server
		/// </summary>
		/// <remarks>Automatically begins listening to tick events</remarks>
		[OverrideMustCallBase(BaseCallMustBeFirstStatement = true)]
		public virtual void OnStartBoth() {
			TimeManager.OnPreTick += PreTick;
			TimeManager.OnTick += Tick;
			TimeManager.OnPostTick += PostTick;
		}

		[OverrideMustCallBase(BaseCallMustBeFirstStatement = true)]
		public override void OnStopServer() {
			base.OnStopServer();
			OnStopBoth();
		}

		[OverrideMustCallBase(BaseCallMustBeFirstStatement = true)]
		public override void OnStopClient() {
			base.OnStopClient();
			OnStopBoth();
		}
		
		/// <summary>
		///		Function called when the object this component is attached to is destroyed on either the client or the server
		/// </summary>
		/// <remarks>Unregisters tick events</remarks>
		[OverrideMustCallBase(BaseCallMustBeFirstStatement = true)]
		public virtual void OnStopBoth() {
			TimeManager.OnPreTick -= PreTick;
			TimeManager.OnTick -= Tick;
			TimeManager.OnPostTick -= PostTick;
		}

		[OverrideMustCallBase(BaseCallMustBeFirstStatement = true)]
		public override void OnOwnershipServer(NetworkConnection prevOwner) {
			base.OnOwnershipServer(prevOwner);
			OnOwnershipBoth(prevOwner);
		}

		[OverrideMustCallBase(BaseCallMustBeFirstStatement = true)]
		public override void OnOwnershipClient(NetworkConnection prevOwner) {
			base.OnOwnershipClient(prevOwner);
			OnOwnershipBoth(prevOwner);
		}
		
		/// <summary>
		///		Function called when the ownership of this object changes on either the client or the server
		/// </summary>
		/// <param name="prevOwner">Previous owner of this object</param>
		public virtual void OnOwnershipBoth(NetworkConnection prevOwner) { }
		
		/// <summary>
		///		Called right before a tick occurs, as well before data is read.
		/// </summary>
		public virtual void PreTick() { }
		
		/// <summary>
		///		Called when a tick occurs.
		/// </summary>
		public virtual void Tick() { }
		
		/// <summary>
		///		Called after a tick occurs; physics would have simulated if using PhysicsMode.TimeManager.
		/// </summary>
		public virtual void PostTick() { }
		
		
		/// <summary>
		///		Function that requests that the server give the provided NetworkConnection ownership
		/// </summary>
		/// <param name="newOwner">Connection the server should transfer ownership to</param>
		public void RequestGiveOwnership(NetworkConnection newOwner) {
			if (IsServer) GiveOwnership(newOwner);
			else GiveOwnershipServerRPC(newOwner);
		}

		/// <summary>
		///		Server RPC that gives the provided NetworkConnection ownership over the controlling object
		/// </summary>
		/// <param name="newOwner">Connection the server should transfer ownership to</param>
		[ServerRpc(RequireOwnership = false)]
		protected void GiveOwnershipServerRPC(NetworkConnection newOwner) => GiveOwnership(newOwner);
	}
}

