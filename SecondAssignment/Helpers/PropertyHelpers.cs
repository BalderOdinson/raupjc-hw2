namespace SecondAssignment.Helpers
{
    public static class PropertyHelpers
    {
        public static void PropertyMapper<T>(this T propertyToMap, T mapToProperty)
        {
            foreach (var property in typeof(T).GetProperties())
            {
                if (property.CanRead && property.CanWrite)
                    property.SetValue(propertyToMap, property.GetValue(mapToProperty));
            }
        }
    }
}
