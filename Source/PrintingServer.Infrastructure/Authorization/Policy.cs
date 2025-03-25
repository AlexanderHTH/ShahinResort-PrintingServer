namespace PrintingServer.Infrastructure.Authorization;

public class Policy
{
    //these are the properties names of an AppUser to be used as policeis
    public const string TokenVersion = "TokenVersion";
    public const string UserName = "UserName";
    public const string PolicyName2 = "PolicyName2";
    public const string PolicyName3 = "PolicyName3";
    private readonly static List<string> policyList = [
                                                        TokenVersion,
                                                        UserName,
                                                        PolicyName2,
                                                        PolicyName3
                                                      ];

    public static List<string> PolicyList { get => policyList; }
}
