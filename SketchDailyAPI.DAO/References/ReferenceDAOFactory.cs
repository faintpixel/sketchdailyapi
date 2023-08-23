using SketchDailyAPI.Models.References.Animals;
using SketchDailyAPI.Models.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SketchDailyAPI.Models.References.People;
using SketchDailyAPI.Models.References.Structures;
using SketchDailyAPI.Models.References.Vegetation;
using SketchDailyAPI.DAO.Queryables;
using SketchDailyAPI.Models;

namespace SketchDailyAPI.DAO.References
{
    public class ReferenceDAOFactory
    {
        public static ReferenceDAO<AnimalReference, AnimalClassifications> GetAnimalsDAO(AppSettings appSettings)
        {
            return new ReferenceDAO<AnimalReference, AnimalClassifications>(ReferenceType.Animal, new AnimalsQueryable(), appSettings);
        }

        public static ReferenceDAO<FullBodyReference, FullBodyClassifications> GetFullBodiesDAO(AppSettings appSettings)
        {
            return new ReferenceDAO<FullBodyReference, FullBodyClassifications>(ReferenceType.FullBody, new FullBodiesQueryable(), appSettings);
        }

        public static ReferenceDAO<BodyPartReference, BodyPartClassifications> GetBodyPartsDAO(AppSettings appSettings)
        {
            return new ReferenceDAO<BodyPartReference, BodyPartClassifications>(ReferenceType.BodyPart, new BodyPartsQueryable(), appSettings);
        }

        public static ReferenceDAO<StructureReference, StructureClassifications> GetStructuresDAO(AppSettings appSettings)
        {
            return new ReferenceDAO<StructureReference, StructureClassifications>(ReferenceType.Structure, new StructuresQueryable(), appSettings);
        }

        public static ReferenceDAO<VegetationReference, VegetationClassifications> GetVegetationsDAO(AppSettings appSettings)
        {
            return new ReferenceDAO<VegetationReference, VegetationClassifications>(ReferenceType.Vegetation, new VegetationQueryable(), appSettings);
        }
    }
}
