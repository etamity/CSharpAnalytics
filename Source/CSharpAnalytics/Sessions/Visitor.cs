﻿using System;

namespace CSharpAnalytics.Sessions
{
    /// <summary>
    /// Represents a Visitor or user of the application.
    /// </summary>
    public class Visitor
    {
        private readonly int id;
        private readonly DateTimeOffset firstVisitAt;

        /// <summary>
        /// Create a brand-new Visitor.
        /// </summary>
        internal Visitor()
            : this(NewId(), DateTimeOffset.Now)
        {
        }

        /// <summary>
        /// Create an existing Visitor.
        /// </summary>
        /// <param name="id">Unique Id of the existing Visitor.</param>
        /// <param name="firstVisitAt">When the first visit occured.</param>
        internal Visitor(int id, DateTimeOffset firstVisitAt)
        {
            this.id = id;
            this.firstVisitAt = firstVisitAt;
        }

        /// <summary>
        /// Unique Id of this user.
        /// </summary>
        public int Id { get { return id; } }

        /// <summary>
        /// Earliest recorded visit for this Visitor.
        /// </summary>
        public DateTimeOffset FirstVisitAt { get { return firstVisitAt; } }

        /// <summary>
        /// Create a new unique Id for this user.
        /// </summary>
        /// <returns>Unique Id for this user.</returns>
        private static int NewId()
        {
            return (new Random().Next() ^ (Guid.NewGuid().GetHashCode() & int.MaxValue));
        }
    }
}