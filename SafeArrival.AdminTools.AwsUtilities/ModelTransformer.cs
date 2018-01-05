using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Amazon.EC2.Model;

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
                try
                {
                    var awsModelProp = awsModelProps.Find(o => o.Name == safeArrivalModelProp.Name);
                    if (awsModelProp != null)
                    {
                        if (awsModelProp.PropertyType.FullName != "System.String" &&
                            safeArrivalModelProp.PropertyType.FullName == "System.String")
                        {
                            safeArrivalModelProp.SetValue(safeArrivalModel, awsModelProp.GetValue(awsModel).ToString());
                        }
                        else
                        {
                            safeArrivalModelProp.SetValue(safeArrivalModel, awsModelProp.GetValue(awsModel));
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    throw ex;
                }
            }
            return safeArrivalModel;
        }

        public static List<S> TransformAwsModelListToSafeArrivalModelList(List<A> awsModels)
        {
            var safeArrivalModels = new List<S>();
            foreach (var awsModel in awsModels)
            {
                safeArrivalModels.Add(TransformAwsModelToSafeArrivalModel(awsModel));
            }
            return safeArrivalModels;
        }
    }
}
