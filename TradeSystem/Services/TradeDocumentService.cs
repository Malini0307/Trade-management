using Microsoft.EntityFrameworkCore;
using TradeSystem.Data;
using TradeSystem.Interfaces;
using TradeSystem.Models;


namespace TradeSystem.Services
{
    public class TradeDocumentService : ITradeDocumentService
    {
        private readonly TfmsDbContext _context;
        private readonly ILogger<TradeDocumentService> _logger;

        public TradeDocumentService(TfmsDbContext context, ILogger<TradeDocumentService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool UploadDocument(TradeDocument doc)
        {
            try
            {
                // Ensure unique ReferenceNumber
                if (_context.TradeDocuments.Any(d => d.ReferenceNumber == doc.ReferenceNumber))
                {
                    _logger.LogWarning("Duplicate ReferenceNumber: {refNum}", doc.ReferenceNumber);
                    return false;
                }

                // Always set server timestamps and defaults
                doc.UploadDate = DateTime.Now;
                if (doc.Status == 0) doc.Status = TdStatus.Active;

                _context.TradeDocuments.Add(doc);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading document");
                return false;
            }
        }

        public TradeDocument? ViewDocument(int id)
        {
            return _context.TradeDocuments
                .Include(t => t.LetterOfCredit)
                .Include(t => t.BankGuarantee)
                .FirstOrDefault(t => t.DocumentId == id);
        }

        public IEnumerable<TradeDocument> GetAllDocumentsById()
        {
            return _context.TradeDocuments
                .Include(t => t.LetterOfCredit)
                .Include(t => t.BankGuarantee)
                .OrderByDescending(t => t.UploadDate)
                .ToList();
        }

        public bool UpdateDocumentDetails(TradeDocument updatedDoc)
        {
            var existing = _context.TradeDocuments.Find(updatedDoc.DocumentId);
            if (existing == null) return false;

            existing.DocumentType = updatedDoc.DocumentType;
            existing.Status = updatedDoc.Status;
            existing.LcId = updatedDoc.LcId;
            existing.GuaranteeId = updatedDoc.GuaranteeId;

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating document {docId}", updatedDoc.DocumentId);
                return false;
            }
        }

    }
}












