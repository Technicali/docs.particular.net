using System;
using System.Collections.Generic;
using NServiceBus.Saga;

namespace NHibernateSagaMapping
{
    public class ConventionMappedSaga : Saga<ConventionMappedSagaData>, IAmStartedByMessages<SagaStarter>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ConventionMappedSagaData> mapper)
        {
        }

        public void Handle(SagaStarter message)
        {
            Data.NestedCollection = new List<NestedCollectionItem>()
            {
                new NestedCollectionItem
                {
                    NestedCollection = new List<DeeplyNestedCollectionItem>()
                    {
                        new DeeplyNestedCollectionItem(),
                        new DeeplyNestedCollectionItem(),
                        new DeeplyNestedCollectionItem(),
                    }
                }
            };
            //Data.NestedEntity = new NestedEntity();
        }
    }
    
    public class ConventionMappedSagaData : ContainSagaData
    {
        public virtual NestedEntity NestedEntity { get; set; }
        public virtual IList<NestedCollectionItem> NestedCollection { get; set; } 
    }

    public class NestedEntity
    {
        public virtual Guid Id { get; set; }
    }

    public class NestedCollectionItem
    {
        public virtual Guid Id { get; set; }
        public virtual IList<DeeplyNestedCollectionItem> NestedCollection { get; set; }
    }

    public class DeeplyNestedCollectionItem
    {
        public virtual Guid Id { get; set; }
    }

}