using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class TextInputHelper {
	private static readonly int nameMax = 10;

	private static readonly string[] deleteWord = 
        new string[]{
			"\n",
			"\r",
			"\t",
			"\r\n"
		};

    public static InputField getText(InputField field){
		foreach (var word in deleteWord) {
            field.text.Replace(word, "");
		}

        if (field.text.Length > nameMax)
            field.text.Remove(nameMax);

        return field;
    }
}
