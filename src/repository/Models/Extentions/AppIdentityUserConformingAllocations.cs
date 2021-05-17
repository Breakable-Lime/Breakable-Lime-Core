namespace BreakableLime.Repository.Models.Extentions
{
    public static class AppIdentityUserConformingAllocations
    {
        public static bool IsConformingToImageAllocation(this ApplicationIdentityUser user) =>
            user.OwnedImages.Count <= user.ImageAllocation;

    }
}