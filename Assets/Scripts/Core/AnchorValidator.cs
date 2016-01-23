using UnityEngine;
using System.Collections;

public class AnchorValidator
{

	private AnchorValidator anchorValidator;

	private AnchorValidator() {

	}

	public static AnchorValidator Instance {
		get {
			if (this.anchorValidator == null) {
				this.anchorValidator = new AnchorValidator();
			}
			return this.anchorValidator;
		}
	}

	public bool Validate(string object1, string anchor1, string object2, string anchor2) {

	}
}

