using System.Linq;
using System.Reflection;

namespace CodePractice.Framework.Tools;

public static class Mapper
{

    public static void MatchAndMap<TSource, TDestination>(this TSource source, TDestination destination, string Roles = null) where TSource : class, new() where TDestination : class, new()
    {
        List<MapModel> maps = new List<MapModel>();
        if (!string.IsNullOrEmpty(Roles))
        {
            var rr = Roles.Split(',');
            foreach (var i in rr)
            {
                try
                {
                    var rrr = i.Split(':');
                    maps.Add(new MapModel() { SourceColumn = rrr[0], DestinationColumn = rrr[1] });
                }
                catch
                {

                }
            }
        }
        if (source != null && destination != null)
        {
            List<PropertyInfo> sourceProperties = source.GetType().GetProperties().ToList<PropertyInfo>();
            List<PropertyInfo> destinationProperties = destination.GetType().GetProperties().ToList<PropertyInfo>();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                PropertyInfo destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);
                if (destinationProperty == null)
                {
                    var role = maps.FirstOrDefault(x => x.SourceColumn == sourceProperty.Name);
                    if (role != null)
                    {
                        destinationProperty = destinationProperties.Find(item => item.Name == role.DestinationColumn);
                    }
                }
                if (destinationProperty != null)
                {

                    try
                    {
                        destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
                if (sourceProperty.Name.Contains("Id"))
                {

                    try
                    {
                        PropertyInfo destinationPropertyID = destinationProperties.Find(item => item.Name == sourceProperty.Name.Replace("Id", "Title"));
                        if (destinationPropertyID != null)
                        {
                            destinationPropertyID.SetValue(destination, GetPropValue(source, sourceProperty.Name.Replace("Id", ".Title")), null);

                        }

                    }
                    catch (System.Exception ex)
                    {

                    }
                }


            }
        }

    }
    public static object GetPropValue(this object src, string propName)
    {
        if (src == null) throw new ArgumentException("Value cannot be null.", "src");
        if (propName == null) throw new ArgumentException("Value cannot be null.", "propName");

        if (propName.Contains("."))//complex type nested
        {
            var temp = propName.Split(new char[] { '.' }, 2);
            return GetPropValue(GetPropValue(src, temp[0]), temp[1]);
        }
        else
        {
            var prop = src.GetType().GetProperty(propName);
            return prop != null ? prop.GetValue(src, null) : null;
        }
    }

    public static TDestination MapProperties<TDestination>(this object source, string Roles = null)
        where TDestination : class, new()
    {
        var destination = Activator.CreateInstance<TDestination>();
        MatchAndMap(source, destination, Roles);

        return destination;
    }

    public static List<TDestination> MapPropertiesForCollection<TDestination>(
        this IEnumerable<object> source, string Roles = null)
     where TDestination : class, new()
    {
        var destinations = new List<TDestination>();
        foreach (var item in source)
        {
            var destination = Activator.CreateInstance<TDestination>();
            MatchAndMap(item, destination, Roles);
            destinations.Add(destination);
        }
        return destinations;
    }

    public static void SetValue<TDestination>(this object source, string Roles = null)
         where TDestination : class, new()
    {
        var destination = Activator.CreateInstance<TDestination>();
    }


    public static void SetPropertyValue(this object p_object, string p_propertyName, object value)
    {
        PropertyInfo property = p_object.GetType().GetProperty(p_propertyName);
        Type t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
        object safeValue = (value == null) ? null : Convert.ChangeType(value, t);

        property.SetValue(p_object, safeValue, null);
    }
    public static void CopyProperties(this object source, object destination)
    {
        // If any this null throw an exception
        if (source == null || destination == null)
            throw new System.Exception("Source or/and Destination Objects are null");
        // Getting the Types of the objects
        Type typeDest = destination.GetType();
        Type typeSrc = source.GetType();

        // Iterate the Properties of the source instance and  
        // populate them from their desination counterparts  
        PropertyInfo[] srcProps = typeSrc.GetProperties();
        foreach (PropertyInfo srcProp in srcProps)
        {
            if (!srcProp.CanRead)
            {
                continue;
            }
            if (srcProp.GetType() == typeof(List<>))
            {
                continue;
            }
            PropertyInfo targetProperty = typeDest.GetProperty(srcProp.Name);
            if (targetProperty == null)
            {
                continue;
            }
            if (!targetProperty.CanWrite)
            {
                continue;
            }
            if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true).IsPrivate)
            {
                continue;
            }
            if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
            {
                continue;
            }
            if (!targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType))
            {
                continue;
            }
            // Passed all tests, lets set the value
            targetProperty.SetValue(destination, srcProp.GetValue(source, null), null);
        }
    }

    public static TDestination MapPropertiesForEditation<TDestination>(this object source, TDestination destination = null)
       where TDestination : class, new()
    {
        if (destination == null)
            destination = Activator.CreateInstance<TDestination>();

        MatchAndMapForEditation(source, destination);

        return destination;
    }
    public static void MatchAndMapForEditation<TSource, TDestination>(this TSource source, TDestination destination) where TSource : class, new() where TDestination : class, new()
    {
        List<MapModel> maps = new List<MapModel>();

        if (source != null && destination != null)
        {
            List<PropertyInfo> sourceProperties = source.GetType().GetProperties().ToList<PropertyInfo>();
            List<PropertyInfo> destinationProperties = destination.GetType().GetProperties().ToList<PropertyInfo>();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {


                PropertyInfo destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);
                if (destinationProperty == null)
                {
                    var role = maps.FirstOrDefault(x => x.SourceColumn == sourceProperty.Name);
                    if (role != null)
                    {
                        destinationProperty = destinationProperties.Find(item => item.Name == role.DestinationColumn);
                    }
                }
                if (destinationProperty != null)
                {

                    try
                    {
                        destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                    }
                    catch (System.Exception ex)
                    {

                    }
                }



            }
        }

    }

}
