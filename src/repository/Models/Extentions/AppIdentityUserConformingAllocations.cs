namespace BreakableLime.Repository.Models.Extentions
{
    public static class AppIdentityUserConformingAllocations
    {
        public static bool IsConformingToIncrementedImageAllocation(this ApplicationIdentityUser user, int incrementation = 1) =>
            user.OwnedImages.Count + incrementation <= user.ImageAllocation;

    }
}