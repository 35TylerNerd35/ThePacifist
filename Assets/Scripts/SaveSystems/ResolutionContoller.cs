using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ResolutionContoller : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    private const string ResolutionPrefsKey = "ResolutionSetting";

    void Start()
    {
        // Populate the TMP Dropdown with available resolutions
        PopulateResolutionsDropdown();

        // Subscribe to the onValueChanged event of the TMP Dropdown
        if (resolutionDropdown != null)
        {
            resolutionDropdown.onValueChanged.AddListener(ChangeResolution);
        }
    }

    // Method to populate the TMP Dropdown with available resolutions
    void PopulateResolutionsDropdown()
    {
        resolutionDropdown.ClearOptions();

        // Get available screen resolutions
        Resolution[] resolutions = Screen.resolutions;

        // Create a HashSet to store unique resolutions
        HashSet<string> uniqueResolutions = new HashSet<string>();

        // Add each resolution as an option to the list
        foreach (Resolution resolution in resolutions)
        {
            string optionText = resolution.width + "x" + resolution.height;

            // Check if the resolution is not already in the list
            if (!uniqueResolutions.Contains(optionText))
            {
                TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData(optionText);
                resolutionDropdown.options.Add(optionData);
                uniqueResolutions.Add(optionText);
            }
        }

        // Set the default value to the saved resolution or the current resolution
        int savedResolutionIndex = PlayerPrefs.GetInt(ResolutionPrefsKey, GetResolutionIndex(Screen.currentResolution, uniqueResolutions));
        resolutionDropdown.value = savedResolutionIndex;
        ChangeResolution(savedResolutionIndex);
    }

    // Method to get the index of a resolution in the TMP Dropdown options
    int GetResolutionIndex(Resolution currentResolution, HashSet<string> uniqueResolutions)
    {
        string currentResolutionText = currentResolution.width + "x" + currentResolution.height;

        if (uniqueResolutions.Contains(currentResolutionText))
        {
            // Find the index of the current resolution in the unique resolutions list
            string[] resolutionArray = new List<string>(uniqueResolutions).ToArray();
            return System.Array.IndexOf(resolutionArray, currentResolutionText);
        }

        return -1;
    }

    // Method to change the screen resolution based on the TMP Dropdown selection
    void ChangeResolution(int index)
    {
        List<string> uniqueResolutions = new List<string>(resolutionDropdown.options.Count);

        foreach (var option in resolutionDropdown.options)
        {
            uniqueResolutions.Add(option.text);
        }

        if (index >= 0 && index < uniqueResolutions.Count)
        {
            string selectedResolutionText = uniqueResolutions[index];
            string[] dimensions = selectedResolutionText.Split('x');
            int width = int.Parse(dimensions[0]);
            int height = int.Parse(dimensions[1]);

            // Set the selected resolution
            Screen.SetResolution(width, height, Screen.fullScreen);

            // Save the resolution setting to PlayerPrefs
            PlayerPrefs.SetInt(ResolutionPrefsKey, index);
            PlayerPrefs.Save(); // Save PlayerPrefs to disk
        }
    }
}
