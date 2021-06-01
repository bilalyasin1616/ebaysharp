using System;
using System.Collections.Generic;
using System.Text;

namespace Ebaysharp.Entities
{
    public class Category
    {
        public string categoryId { get; set; }
        public string categoryName { get; set; }

    }
    public class CategoryTreeBase
    {
        public string categoryTreeId { get; set; }
        public string categoryTreeVersion { get; set; }
    }

    public class CategoryTree : CategoryTreeBase
    {
        public ChildCategoryNode rootCategoryNode { get; set; }
    }

    public class ChildCategoryNode
    {
        public Category category { get; set; }
        public int? categoryTreeNodeLevel { get; set; }
        public List<ChildCategoryNode> childCategoryTreeNodes { get; set; }
    }

    public class AspectConstraint
    {
        public List<string> aspectApplicableTo { get; set; }
        public enum AspectApplicableTo
        {
            ITEM, PRODUCT
        }
        public string aspectDataType { get; set; }
        public enum AspectDataType
        {
            DATE, NUMBER, STRING, STRING_ARRAY
        }
        public bool? aspectEnabledForVariations { get; set; }
        public string aspectFormat { get; set; }
        public int aspectMaxLength { get; set; }
        public string aspectMode { get; set; }
        public enum AspectMode
        {
            FREE_TEXT, SELECTION_ONLY
        }
        public bool? aspectRequired { get; set; }
        public string aspectUsage { get; set; }
        public enum AspectUsage
        {
            RECOMMENDED, OPTIONAL
        }
        public string itemToAspectCardinality { get; set; }
        public enum ItemToAspectCardinality
        {
            MULTI, SINGLE
        }
    }

    public class ValueConstraint
    {
        public string applicableForLocalizedAspectName { get; set; }
        public List<string> applicableForLocalizedAspectValues { get; set; }
    }

    public class AspectValue
    {
        public string localizedValue { get; set; }
        public List<ValueConstraint> valueConstraints { get; set; }
    }

    public class Aspect
    {
        public AspectConstraint aspectConstraint { get; set; }
        public List<AspectValue> aspectValues { get; set; }
        public string localizedAspectName { get; set; }
    }

    public class ItemAspectsForCategory
    {
        public List<Aspect> aspects { get; set; }
    }

}
