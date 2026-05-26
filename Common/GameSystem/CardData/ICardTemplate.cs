using Terraria;
using Terraria.ModLoader;
using TerraTCG.Common.GameSystem.GameState;
using TerraTCG.Content.Gores;

namespace TerraTCG.Common.GameSystem.CardData
{
	public interface ICardTemplate
	{
		Card Card { get; }
		Card CreateCard();
	}


	public abstract class BaseCardTemplate : ModSystem, ICardTemplate
	{
		private Card _card;
		public Card Card
		{
			get
			{
				bool newCard = _card == null;
				_card ??= CreateCard();
				if (newCard)
				{
					if (!Main.dedServ)
					{
						var goreInstance = new CardGore(_card.Name, _card.TexturePath);
						Mod.AddContent(goreInstance);
						_card.GoreType = goreInstance.Type;
					}
				}
				return _card;
			}
		}

		public abstract Card CreateCard();

		public override void OnModLoad()
		{
			// Guarantee that it loads on time
			_ = Card;
		}
	}
}
