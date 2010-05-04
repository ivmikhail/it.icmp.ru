using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace ITCommunity {

	/// <summary>
	/// Общий рейтинг
	/// </summary>
	public class Rating {

		public enum EntityType {
			Comment = 0,
			Post = 1,
			User = 2
		}

		#region For caching

		private delegate object RatingLoader(int entityId, EntityType type);

		private static RatingLoader _ratingLoader = GetFromDB;

		#endregion

		#region Properties

		private int _id;
		private int _entityId;
		private EntityType _type;
		private int _value;

		public int Id {
			get { return _id; }
		}

		public int EntityId {
			get { return _entityId; }
			set { _entityId = value; }
		}

		public EntityType Type {
			get { return _type; }
			set { _type = value; }
		}

		public int Value {
			get { return _value; }
			set { _value = value; }
		}

		#endregion

		#region Constructors

		public Rating() {
			_id = 0;
			_entityId = 0;
			_type = EntityType.Comment;
			_value = 0;
		}

		private Rating(int id, int entityId, EntityType type, int value) {
			_id = id;
			_entityId = entityId;
			_type = type;
			_value = value;
		}

		#endregion

		#region Public static methods

		public static Rating Get(int entityId, EntityType type) {
			string cacheName = Config.Get("RatingCacheNamePrefix") + type.ToString() + entityId.ToString();
			double cacheTime = Config.GetDouble("RatingCachePer");

			var rating = AppCache.Get(
				cacheName,
				_ratingLoader,
				new object[] { entityId, type },
				cacheTime
			);

			return (Rating)rating;
		}

		public static Rating Add(int entityId, EntityType type, int userId, int value) {
			Database.RatingLogAdd(entityId, (int)type, userId, value);

			Rating rating = Get(entityId, type);
			if (rating.Id == 0) {
				rating = GetFromRow(Database.RatingAdd(entityId, (int)type, value));
			}
			else {
				rating.Value += value;
				Database.RatingUpdateValue(rating.Id, rating.Value);
			}

			return rating;
		}

		public static bool IsUserVoted(int entityId, EntityType type, int userId) {
			DataRow dr = Database.RatingLogGetByEntity(entityId, (int)type, userId);
			return (dr != null);
		}

		#endregion

		#region Private static methods

		private static Rating GetFromDB(int entityId, EntityType type) {
			Rating rating = GetFromRow(Database.RatingGetByEntity(entityId, (int)type));

			rating.EntityId = entityId;
			rating.Type = type;

			return rating;
		}

		private static Rating GetFromRow(DataRow dr) {
			Rating rating;

			if (dr == null) {
				rating = new Rating();
			}
			else {
				rating = new Rating(
					Convert.ToInt32(dr["id"]),
					Convert.ToInt32(dr["entity_id"]),
					(EntityType)Convert.ToInt32(dr["entity_type"]),
					Convert.ToInt32(dr["value"])
				);
			}

			return rating;
		}

		#endregion

	}
}
