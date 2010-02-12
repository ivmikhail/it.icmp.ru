﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace ITCommunity {

	public enum EntityType {
		Comment = 0,
		Post = 1,
		User = 2
	}

	/// <summary>
	/// Общий рейтинг
	/// </summary>
	public class Rating {

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

		public static Rating Get(int entityId, EntityType type) {
			return GetRatingFromRow(Database.RatingGetByEntity(entityId, (int)type));
		}

		public static Rating Add(int entityId, EntityType type, int userId, int value) {
			Database.RatingLogAdd(entityId, (int)type, userId, value);
			Rating rating = Get(entityId, type);
			if (rating.Id == 0) {
				return GetRatingFromRow(Database.RatingAdd(entityId, (int)type, value));
			}
			else {
				rating.Value += value;
				Database.RatingUpdateValue(rating.Id, rating.Value);
			}
			return rating;
		}

		private static Rating GetRatingFromRow(DataRow dr) {
			Rating rating;
			if (dr == null) {
				rating = new Rating();
			}
			else {
				rating = new Rating(Convert.ToInt32(dr["id"]),
									Convert.ToInt32(dr["entity_id"]),
									(EntityType)Convert.ToInt32(dr["entity_type"]),
									Convert.ToInt32(dr["value"]));
			}

			return rating;
		}
	}
}