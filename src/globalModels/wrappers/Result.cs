using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

namespace BreakableLime.GlobalModels.Wrappers {
    public class Result<T> where T : class
    {
        public IList<string> Errors { get; set; } = new List<string>();
        public bool IsSuccessful => !Errors.Any();
        
        public T Product { get; set; }
        
        
        //------- STATICS -------//

        public static Result<TT> FromError<TT>(string error) where TT : class => 
            FromError<TT>(new List<string> {error});

        public static Result<TT> FromError<TT>(IList<string> error) where TT : class =>
            new Result<TT>
            {
                Errors = error
            };

        public static Result<TT> FromResult<TT>(TT result) where TT : class =>
            new Result<TT>
            {
                Product = result
            };
    }
}
