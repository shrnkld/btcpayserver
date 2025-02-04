using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BTCPayServer.Data
{
    public class WebhookDeliveryData : IHasBlobUntyped
    {
        [Key]
        [MaxLength(25)]
        public string Id { get; set; }
        [MaxLength(25)]
        [Required]
        public string WebhookId { get; set; }
        public WebhookData Webhook { get; set; }

        [Required]
        public DateTimeOffset Timestamp { get; set; }
        [Obsolete("Use Blob2 instead")]
        public byte[] Blob { get; set; }
        public string Blob2 { get; set; }


        internal static void OnModelCreating(ModelBuilder builder, DatabaseFacade databaseFacade)
        {
            builder.Entity<WebhookDeliveryData>()
                .HasOne(o => o.Webhook)
                .WithMany(a => a.Deliveries).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<WebhookDeliveryData>().HasIndex(o => o.WebhookId);

            if (databaseFacade.IsNpgsql())
            {
                builder.Entity<WebhookDeliveryData>()
                    .Property(o => o.Blob2)
                    .HasColumnType("JSONB");
            }
        }
    }
}
