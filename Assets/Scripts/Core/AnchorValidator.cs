using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using System.Collections;

public class AnchorValidator
{
	private static AnchorValidator anchorValidator;
	private Dictionary <string, Dictionary <string, HashSet<string>>> connections;

	private AnchorValidator() {
		TextAsset anchorsFile = Resources.Load ("Anchors") as TextAsset	;
		this.connections = new Dictionary <string, Dictionary <string, HashSet<string>>> ();

		string[] lines = anchorsFile.text.Split (new char[] { '\n' });
		foreach (string line in lines) {
			string[] entities = line.Trim().Split(new char[] {'-'});
			Assert.AreEqual(entities.Length, 2, "Line do not have - symbol");
			if (entities [1].IndexOf ("|") >= 0) {
				string[] parsedObject = parseObject (entities [0]);
				foreach (string entity in parseOrLine(entities[1])) {
					Add (parsedObject, entity);
					string[] otherParsedObject = parseObject (entity);
					Add (otherParsedObject, entities [0]);
					Add (otherParsedObject, null);
				}
			} else if (entities [1].IndexOf ("&") >= 0) {
				string[] group1 = parseAndLine(entities[0]);
				string[] group2 = parseAndLine(entities[1]);
				Assert.AreEqual(group1.Length, group2.Length, "Line with & needs to contains the same number of objects of each sides of the -");
				for (int i = 0; i < group1.Length; ++i) {
					string[] group1Parsed = parseObject (group1 [i]);
					for (int j = 0; j < group2.Length; j++) {
						Add (group1Parsed, group2 [j]);
						Add (parseObject (group2 [j]), group1 [i]);
					}
				}
			} else {
				Add(parseObject (entities [0]), entities[1]);
				Add(parseObject (entities [1]), entities[0]);
			}

		}
	}

	private void printConnections() {
		string connect = "";
		foreach(var obj in this.connections) {
			connect += obj.Key + "_\n";
			foreach (var anchors in obj.Value) {
				connect += "____" + anchors.Key +"_\n";
				foreach (var objAnchor in anchors.Value) {
					if (objAnchor == null) {
						connect += "____________NULL_\n";
					} else {
						connect += "____________" + objAnchor+"_\n";
					}
				}
			}
		}
		Debug.Log (connect);
	}

	private void Add(string[] objectAnchor, string object2anchor) {	
		Dictionary<string, HashSet<string>> anchors;
		if (!this.connections.TryGetValue (objectAnchor [0], out anchors)) {
			anchors = new Dictionary<string, HashSet<string>> ();
			this.connections.Add (objectAnchor [0], anchors);
		}
		HashSet<string> linked;
		if (!anchors.TryGetValue (objectAnchor [1], out linked)) {
			linked = new HashSet<string> ();
			anchors.Add (objectAnchor[1], linked);
		}
		linked.Add(object2anchor);
	}

	private string[] parseObject(string obj) {
		string[] objectAnchor = obj.Split (new char[] { '.' });
		Assert.AreEqual (objectAnchor.Length, 2, "Declaration must be object.anchor");
		return objectAnchor;
	}

	private string[] parseOrLine(string orLine) {
		string[] objectsAnchors = orLine.Split (new char[] { '|' });
		Assert.IsTrue (objectsAnchors.Length >= 2, "Line with | needs to contains at least two objects");
		return objectsAnchors;
	}

	private string[] parseAndLine(string andLine) {
		string[] objectsAnchors = andLine.Split (new char[] { '&' });
		Assert.IsTrue (objectsAnchors.Length >= 2, "Line with & needs to contains at least two objects");
		return objectsAnchors;
	}

	public static AnchorValidator Instance {
		get {
			if (AnchorValidator.anchorValidator == null) {
				AnchorValidator.anchorValidator = new AnchorValidator();
			}
			return AnchorValidator.anchorValidator;
		}
	}

	public bool Validate(string object1, string anchor1, string object2Anchor2 = null) {
		Dictionary<string, HashSet<string>> objectAnchors;
		if (!this.connections.TryGetValue (object1, out objectAnchors)) {
			return object2Anchor2 == null;
		}

		HashSet<string> validOtherObjectAnchor;
		if (!objectAnchors.TryGetValue (anchor1, out validOtherObjectAnchor)) {
			return object2Anchor2 == null;
		}

		return validOtherObjectAnchor.Contains (object2Anchor2);
	}
}