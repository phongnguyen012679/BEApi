using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEApi.Model
{
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User user { get; set; }

        public string Token { get; set; }

        public string JwtId { get; set; }

        public bool IsUsed { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime IssuedAt { get; set; }

        public DateTime ExpiredAt { get; set; }
    }
}