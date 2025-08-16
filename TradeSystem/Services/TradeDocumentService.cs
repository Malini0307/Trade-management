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
        private static readonly object _rngLock = new object();
        private static readonly Random _rng = new Random();

        public TradeDocumentService(TfmsDbContext context, ILogger<TradeDocumentService> logger)
        {
            _context = context;
            _logger = logger;
        }

        private string GenerateUniqueReference()
        {
            string candidate;
            int attempts = 0;
            do
            {
                lock (_rngLock)
                {
                    candidate = "ABC" + _rng.Next(10000, 99999);
                }
                attempts++;
                if (attempts > 50)
                {
                    throw new InvalidOperationException("Unable to generate unique reference number after multiple attempts.");
                }
            } while (_context.TradeDocuments.Any(d => d.ReferenceNumber == candidate));
            return candidate;
        }

        public bool UploadDocument(TradeDocument doc)
        {
            try
            {
                // Ensure unique ReferenceNumber (generate if missing or duplicate)
                if (string.IsNullOrWhiteSpace(doc.ReferenceNumber) || _context.TradeDocuments.Any(d => d.ReferenceNumber == doc.ReferenceNumber))
                {
                    doc.ReferenceNumber = GenerateUniqueReference();
                }

                // Always set server timestamps and defaults
                doc.UploadDate = DateTime.Now;
                if (doc.Status == 0) doc.Status = TdStatus.Active;

                _context.TradeDocuments.Add(doc);
                const int maxRetries = 3;
                for (int attempt = 0; attempt < maxRetries; attempt++)
                {
                    try
                    {
                        _context.SaveChanges();
                        return true;
                    }
                    catch (DbUpdateException ex)
                    {
                        // Likely unique constraint violation on ReferenceNumber; try a new one
                        _logger.LogWarning(ex, "Save failed, retrying with new ReferenceNumber (attempt {attempt})", attempt + 1);
                        doc.ReferenceNumber = GenerateUniqueReference();
                        // loop and retry
                    }
                }
                _logger.LogError("Exceeded max retries saving TradeDocument with unique ReferenceNumber.");
                return false;
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












