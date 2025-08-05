using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponCustomizer : MonoBehaviour
{
    [Header("Prefab Parts")]
    public GameObject blade;
    public GameObject rainGuard;
    public GameObject grip;
    public GameObject pommel;
    /*
        [Header("Equiped Parts")]
        public GameObject equipedBlade;
        private int bladeMeshIndex;
        private int bladeMatIndex;
        public GameObject equipedRainGuard;
        private int rainGuardMeshIndex;
        private int rainGuardMatIndex;
        public GameObject equipedGrip;
        private int gripMeshIndex;
        private int gripMatIndex;
        public GameObject equipedPommel;
        private int pommelMeshIndex;
        private int pommelMatIndex;
    */
    [Header("Meshes")]
    public List<Mesh> bladeMeshes;
    public List<Mesh> rainGuardMeshes;
    public List<Mesh> gripMeshes;
    public List<Mesh> pommelMeshes;

    [Header("Materials")]
    public List<Material> bladeMaterials;
    public List<Material> rainGuardMaterials;
    public List<Material> gripMaterials;
    public List<Material> pommelMaterials;

    [Header("UI Elements")]
    public TMP_Dropdown bladeMeshDropdown;
    public TMP_Dropdown bladeMaterialDropdown;
    public TMP_Dropdown rainGuardMeshDropdown;
    public TMP_Dropdown rainGuardMaterialDropdown;
    public TMP_Dropdown gripMeshDropdown;
    public TMP_Dropdown gripMaterialDropdown;
    public TMP_Dropdown pommelMeshDropdown;
    public TMP_Dropdown pommelMaterialDropdown;

    private void Start()
    {
        // Populate dropdowns
        PopulateDropdown(bladeMeshDropdown, bladeMeshes);
        PopulateDropdown(bladeMaterialDropdown, bladeMaterials);
        PopulateDropdown(rainGuardMeshDropdown, rainGuardMeshes);
        PopulateDropdown(rainGuardMaterialDropdown, rainGuardMaterials);
        PopulateDropdown(gripMeshDropdown, gripMeshes);
        PopulateDropdown(gripMaterialDropdown, gripMaterials);
        PopulateDropdown(pommelMeshDropdown, pommelMeshes);
        PopulateDropdown(pommelMaterialDropdown, pommelMaterials);

        // Add listeners
        bladeMeshDropdown.onValueChanged.AddListener(index => ChangeMesh(blade, bladeMeshes, index));
        bladeMaterialDropdown.onValueChanged.AddListener(index => ChangeMaterial(blade, bladeMaterials, index));
        rainGuardMeshDropdown.onValueChanged.AddListener(index => ChangeMesh(rainGuard, rainGuardMeshes, index));
        rainGuardMaterialDropdown.onValueChanged.AddListener(index => ChangeMaterial(rainGuard, rainGuardMaterials, index));
        gripMeshDropdown.onValueChanged.AddListener(index => ChangeMesh(grip, gripMeshes, index));
        gripMaterialDropdown.onValueChanged.AddListener(index => ChangeMaterial(grip, gripMaterials, index));
        pommelMeshDropdown.onValueChanged.AddListener(index => ChangeMesh(pommel, pommelMeshes, index));
        pommelMaterialDropdown.onValueChanged.AddListener(index => ChangeMaterial(pommel, pommelMaterials, index));
    }

    private void PopulateDropdown<T>(TMP_Dropdown dropdown, List<T> items) where T : UnityEngine.Object
    {
        dropdown.ClearOptions();
        List<string> options = new List<string>();
        foreach (var item in items)
        {
            options.Add(item.name); // 'name' is accessible because T is constrained to UnityEngine.Object
        }
        dropdown.AddOptions(options);
    }

    private void ChangeMesh(GameObject part, List<Mesh> meshes, int index)
    {
        if (index >= 0 && index < meshes.Count)
        {
            part.GetComponent<MeshFilter>().mesh = meshes[index];
        }
    }

    private void ChangeMaterial(GameObject part, List<Material> materials, int index)
    {
        if (index >= 0 && index < materials.Count)
        {
            part.GetComponent<MeshRenderer>().material = materials[index];
        }
    }
    
    
    public void UpdateWeaponAppearance()
    {
        // Update blade
        if (blade != null && bladeMeshes != null && bladeMeshes.Count > 0)
        {
            blade.GetComponent<MeshFilter>().mesh = bladeMeshes[0]; // Example: Use the first mesh
            blade.GetComponent<MeshRenderer>().material = bladeMaterials[0]; // Example: Use the first material
        }

        // Update rain guard
        if (rainGuard != null && rainGuardMeshes != null && rainGuardMeshes.Count > 0)
        {
            rainGuard.GetComponent<MeshFilter>().mesh = rainGuardMeshes[0];
            rainGuard.GetComponent<MeshRenderer>().material = rainGuardMaterials[0];
        }

        // Update grip
        if (grip != null && gripMeshes != null && gripMeshes.Count > 0)
        {
            grip.GetComponent<MeshFilter>().mesh = gripMeshes[0];
            grip.GetComponent<MeshRenderer>().material = gripMaterials[0];
        }

        // Update pommel
        if (pommel != null && pommelMeshes != null && pommelMeshes.Count > 0)
        {
            pommel.GetComponent<MeshFilter>().mesh = pommelMeshes[0];
            pommel.GetComponent<MeshRenderer>().material = pommelMaterials[0];
        }

        Debug.Log("Weapon appearance updated!");
    }

}
