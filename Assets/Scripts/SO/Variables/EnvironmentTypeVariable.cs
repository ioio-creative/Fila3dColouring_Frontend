using UnityEngine;
using SOVariables;

namespace Fila3dColouring.SOVariables.Enums
{
    public enum EnvironmentType
    {
        Development,
        Production
    }

    [CreateAssetMenu(menuName = "Fila3dColouring/Variables/Environment Type Variable")]
    public class EnvironmentTypeVariable : EnumVariable<EnvironmentType>
    {
        public bool IsDevelopment()
        {
            return Value == EnvironmentType.Development;
        }

        public bool IsProduction()
        {
            return Value == EnvironmentType.Production;
        }
    }
}
