using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.Static;

namespace BrowserGame.Internal
{
    internal class UpgradeSeeder
    {
        private ApplicationDbContext _context;

        public UpgradeSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task GenerateAsync(UpgradeSeederSettings upgradeSettings)
        {
            var upgradeNew = new UpgradeInfo()
            {
                BuildingName = upgradeSettings.BuildingName,
                ClayCost = upgradeSettings.Clay,
                IronCost = upgradeSettings.Iron,
                WoodCost = upgradeSettings.Wood,
                CropCost = upgradeSettings.Crop,
                Level = 0,
                AdditionalCropUpkeep = 0,
                IsFinnalUpgrade = upgradeSettings.FinalLevel == 0,
                UpgradeDuration = upgradeSettings.BuildTime,
                ValueChangeDecimal = upgradeSettings.Value
            };

            _context.UpgradeInfos.Add(upgradeNew);

            for (int level = 1; level <= upgradeSettings.FinalLevel; ++level)
			{
                UpgradeInfo upgradeOld = upgradeNew;

                decimal costFactor = upgradeSettings.CostScaling * (decimal)Math.Pow((double)upgradeSettings.CostScalingDelta, level);
                decimal upgradeFactor = upgradeSettings.TimeScaling * (decimal)Math.Pow((double)upgradeSettings.TimeScalingDelta, level);

                upgradeNew = new()
                {
                    BuildingName = upgradeSettings.BuildingName,
                    ClayCost = Convert.ToInt32(upgradeOld.ClayCost * costFactor),
                    IronCost = Convert.ToInt32(upgradeOld.IronCost * costFactor),
                    WoodCost = Convert.ToInt32(upgradeOld.WoodCost * costFactor),
                    CropCost = Convert.ToInt32(upgradeOld.CropCost * costFactor),
                    Level = level,
                    AdditionalCropUpkeep = level / 10,
                    IsFinnalUpgrade = level == upgradeSettings.FinalLevel,
                    UpgradeDuration = Convert.ToInt32(upgradeOld.UpgradeDuration * upgradeFactor),
                };

                if (upgradeSettings.UseFixedValue) upgradeNew.ValueChangeDecimal = upgradeSettings.ValueFixedChange;
                else
				{
                    decimal valueFactor = upgradeSettings.ValueScaling * (decimal)Math.Pow((double)upgradeSettings.ValueScalingDelta, level);
                    upgradeNew.ValueChangeDecimal = upgradeOld.ValueChangeDecimal * valueFactor;
                }

                _context.UpgradeInfos.Add(upgradeNew);
			}

            await _context.SaveChangesAsync();
        }
    }
}
