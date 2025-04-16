using Comandas.Domain.Models;
using Comandas.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Services.Interfaces
{
    public interface ICardapioItemServices
    {
        Task DeleteCardapioAsync(int id);
        Task<IEnumerable<CardapioItemDto>> GetCardapioItems();
        Task<CardapioItemDto> GetCardapioItemsAsync(int id);
        Task<CardapioItem> PostCardapioItemAsync(CardapioCreateDto cardapioItemDto);
        Task PutCardapioItemAsync(CardapioUpdateDto cardapioItemDto, int id);
    }
}
