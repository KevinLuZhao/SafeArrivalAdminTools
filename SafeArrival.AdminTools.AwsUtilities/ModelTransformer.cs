using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SafeArrival.AdminTools.AwsUtilities
{
    public class ModelTransformer<A, S>
    {
        public static S TransformAwsModelToSafeArrivalModel(A awsModel)
        {
            List<PropertyInfo> awsModelProps = typeof(A).GetProperties().ToList();
            S safeArrivalModel = (S)Activator.CreateInstance(typeof(S));
            foreach (PropertyInfo safeArrivalModelProp in typeof(S).GetProperties())
            {
                var awsModelProp = awsModelProps.Find(o => o.Name == safeArrivalModelProp.Name);
                safeArrivalModelProp.SetValue(safeArrivalModel, awsModelProp.GetValue(awsModel));
            }
            return safeArrivalModel;
        }
    }
}
