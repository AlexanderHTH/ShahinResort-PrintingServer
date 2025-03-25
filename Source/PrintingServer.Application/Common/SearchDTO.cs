using FluentValidation;
using PrintingServer.Domain.Common;
using PrintingServer.Domain.Exceptions;

namespace PrintingServer.Application.Common;
/// <summary>
/// Represent a Search data (as DTO) to be send in Search API's.
/// </summary>
public class SearchDTO<T>(List<SearchCondition>? conditions, List<ResultSort>? sorts, int pageNumber, int pageSize)/*(List<SearchCondition>? conditions, List<ResultSort>? sorts, int pageNumber, int pageSize) */: /*AbstractValidator<SearchDTO<T>>, */ISearchDTO<T> where T : class
{
    /// <summary>
    /// Representation of SearchCondition record, which can be null if there are no conditions.  
    /// Example: SearchCondition("search_FieldName", "search_FieldValue", (int)search_ConditionType, (int)search_ConditionOperationType)
    /// 
    /// ### search_FieldName: The column name in the database.
    /// 
    /// ### search_FieldValue: The value to (search, compare, etc.) against.
    /// 
    /// ### search_ConditionType: An integer value from the `ConditionsType` enum:
    /// 
    /// - **0**  - GuidEqual  
    /// - **1**  - GuidNotEqual  
    /// - **2**  - BooleanTrue  
    /// - **3**  - BooleanFalse  
    /// - **4**  - String  
    /// - **5**  - StringEqual  
    /// - **6**  - StringNotEqual  
    /// - **7**  - LongEqual  
    /// - **8**  - LongNotEqual  
    /// - **9**  - LongLesserThan  
    /// - **10** - LongLesserThanOrEqual  
    /// - **11** - LongGreaterThan  
    /// - **12** - LongGreaterThanOrEqual  
    /// - **13** - IntegerEqual  
    /// - **14** - IntegerNotEqual  
    /// - **15** - IntegerLesserThan  
    /// - **16** - IntegerLesserThanOrEqual  
    /// - **17** - IntegerGreaterThan  
    /// - **18** - IntegerGreaterThanOrEqual  
    /// - **19** - DecimalEqual  
    /// - **20** - DecimalNotEqual  
    /// - **21** - DecimalLesserThan  
    /// - **22** - DecimalLesserThanOrEqual  
    /// - **23** - DecimalGreaterThan  
    /// - **24** - DecimalGreaterThanOrEqual  
    /// - **25** - FloatEqual  
    /// - **26** - FloatNotEqual  
    /// - **27** - FloatLesserThan  
    /// - **28** - FloatLesserThanOrEqual  
    /// - **29** - FloatGreaterThan  
    /// - **30** - FloatGreaterThanOrEqual  
    /// - **31** - DateEqual  
    /// - **32** - DateNotEqual  
    /// - **33** - DateLesserThan  
    /// - **34** - DateLesserThanOrEqual  
    /// - **35** - DateGreaterThan  
    /// - **36** - DateGreaterThanOrEqual  
    /// 
    /// ### search_ConditionOperationType: An integer value from the `ConditionOperationType` enum (default: 0).
    /// 
    /// - **0** - AND  
    /// - **1** - OR  
    /// </summary>
    public List<SearchCondition>? Conditions { get; set; } = conditions;
    /// <summary>
    /// Representation of ResultSort record, which can be null if there are no sorting.  
    /// Example: ResultSort("sort_FieldName", (int)sort_Direction)
    /// 
    /// ### sort_FieldName: The column name in the database.
    /// 
    /// ### sort_Direction: An integer value from the `SortDirection` enum:
    /// 
    /// - **0**  - ASC  
    /// - **1**  - DESC  
    /// - **99** - Default  
    /// </summary>
    public List<ResultSort>? Sorts { get; set; } = sorts;
    /// <summary>
    /// Representation of page number you want to view from Pagenation.  
    /// </summary>
    public int PageNumber { get; set; } = pageNumber;
    /// <summary>
    /// Representation of items number to be viewed in a single page.  
    /// </summary>
    public int PageSize { get; set; } = pageSize;
}


public class SearchDTOValidator<T> : AbstractValidator<SearchDTOValidator<T>> where T : class
{
    private List<SearchCondition>? Conditions { get; set; } //= conditions;
    private List<ResultSort>? Sorts { get; set; } //= sorts;
    private int PageNumber { get; set; } //= pageNumber;
    private int PageSize { get; set; } //= pageSize;
    public SearchDTOValidator(List<SearchCondition>? conditions = null, List<ResultSort>? sorts = null, int pageNumber = 0, int pageSize = 0)
    {
        Conditions = conditions;
        Sorts = sorts;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public static void SearchDTOValid(ISearchDTO<T> search)
    {
        int[] allowedPageSizes = [0, 5, 10, 15, 30, 50, 75, 100]; 
        var check = new SearchDTOValidator<T>(search.Conditions,search.Sorts, search.PageNumber, search.PageSize);
        check.RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Page number can't be a negative value.");

        check.RuleFor(r => r.PageSize)
            .Must(value => allowedPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", allowedPageSizes)}].");

        check.RuleForEach(r => r.Conditions)
            .ChildRules(conditions =>
            {
                conditions.RuleFor(c => c.Search_FieldName)
                    .Must(fieldName => typeof(T).GetProperties().Any(p => p.Name == fieldName))
                    .WithMessage(c => $"Invalid search field name(not a property in {typeof(T)}): {c.Search_FieldName}");
            });
        check.RuleForEach(r => r.Conditions)
            .ChildRules(conditions =>
            {
                conditions.RuleFor(c => c.Search_ConditionType)
                    .Must(ctype => Enum.IsDefined(typeof(ConditionsType), ctype))
                    .WithMessage(c => $"Invalid condition type: {c.Search_ConditionType}");
            });
        check.RuleForEach(r => r.Sorts)
            .ChildRules(sorts =>
            {
                sorts.RuleFor(c => c.Sort_FieldName)
                    .Must(fieldName => typeof(T).GetProperties().Any(p => p.Name == fieldName))
                    .WithMessage(c => $"Invalid sort field name(not a property in {typeof(T)}): {c.Sort_FieldName}");
            });
        check.RuleForEach(r => r.Sorts)
            .ChildRules(sorts =>
            {
                sorts.RuleFor(r => r.Sort_Direction)
                    .Must(ctype => Enum.IsDefined(typeof(SortDirection), ctype))
                    .WithMessage(c => $"Invalid sort direction: {c.Sort_Direction}");
            });
        var validationResult = check.Validate(check);
        if (!validationResult.IsValid)
        {
            throw new FluentValidationException(validationResult);
        }
    }
}
